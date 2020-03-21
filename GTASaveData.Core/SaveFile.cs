﻿using GTASaveData.Types;
using GTASaveData.Types.Interfaces;
using System;
using System.Diagnostics;
using System.IO;

namespace GTASaveData
{
    // TODO: add ability to add/remove blocks after main game data (make use of padding space)?

    /// <summary>
    /// Represents a saved <i>Grand Theft Auto</i> game.
    /// </summary>
    public abstract class SaveFile : GTAObject, IGTASave
    {
        private static readonly byte[] DefaultPadding = new byte[1] { 0 };

        protected WorkBuffer m_workBuf;
        protected int m_checksum;

        private SaveFileFormat m_fileFormat;
        private PaddingType m_padding;
        private byte[] m_paddingBytes;
        private bool m_disposed;

        public SaveFileFormat FileFormat
        {
            get { return m_fileFormat; }
            set { m_fileFormat = value; OnPropertyChanged(); }
        }

        public PaddingType Padding
        {
            get { return m_padding; }
            set { m_padding = value; OnPropertyChanged(); }
        }

        public byte[] PaddingBytes
        {
            get { return m_paddingBytes; }
            set { m_paddingBytes = value ?? DefaultPadding; }
        }

        public abstract string Name { get; set;  }
        public abstract DateTime TimeLastSaved { get; set; }

        public SaveFile()
        {
            m_fileFormat = SaveFileFormat.Default;
            m_workBuf = new WorkBuffer();
            m_padding = PaddingType.Default;
            m_paddingBytes = DefaultPadding;
            m_disposed = false;
        }

        public void Dispose()
        {
            if (!m_disposed)
            {
                m_workBuf.Dispose();
                m_disposed = true;
            }
        }

        public void Write(string path)
        {
            byte[] data = Serializer.Write(this, m_fileFormat);
            File.WriteAllBytes(path, data);

            Debug.WriteLine("Wrote {0} bytes to disk.", data.Length);
        }

        protected byte[] GetPadding(int length)
        {
            switch (m_padding)
            {
                case PaddingType.Pattern:
                {
                    byte[] pad = new byte[length];
                    byte[] seq = PaddingBytes;

                    for (int i = 0; i < length; i++)
                    {
                        pad[i] = seq[i % seq.Length];
                    }

                    return pad;
                }

                case PaddingType.Random:
                {
                    byte[] pad = new byte[length];
                    Random rand = new Random();

                    rand.NextBytes(pad);
                    return pad;
                }
            }

            return m_workBuf.ToArray(length);
        }

        protected override void ReadObjectData(WorkBuffer buf, SaveFileFormat fmt)
        {
            m_fileFormat = fmt;
            LoadAllData(buf);
        }

        protected override void WriteObjectData(WorkBuffer buf, SaveFileFormat fmt)
        {
            m_fileFormat = fmt;
            SaveAllData(buf);
        }

        protected abstract void LoadAllData(WorkBuffer buf);
        protected abstract void SaveAllData(WorkBuffer buf);
        protected abstract bool DetectFileFormat(byte[] data, out SaveFileFormat fmt);
        protected abstract int WriteBlock(WorkBuffer buf);

        public override string ToString()
        {
            return string.Format("{0}: {{ Name = {1}, Format = {2} }}", GetType().Name, Name, m_fileFormat);
        }

        public static bool GetFileFormat<T>(string path, out SaveFileFormat fmt)
            where T : SaveFile, new()
        {
            byte[] data = File.ReadAllBytes(path);
            return new T().DetectFileFormat(data, out fmt);
        }

        public static T Load<T>(string path)
            where T : SaveFile, new()
        {
            bool valid = GetFileFormat<T>(path, out SaveFileFormat fmt);
            if (!valid)
            {
                return null;
            }

            return Load<T>(path, fmt);
        }

        public static T Load<T>(string path, SaveFileFormat fmt)
            where T : SaveFile, new()
        {
            byte[] data = File.ReadAllBytes(path);
            WorkBuffer buf = new WorkBuffer(data);
            T obj = new T();
            obj.ReadObjectData(buf, fmt);

            return obj;
        }
    }
}
