﻿using System;
using System.Diagnostics;
using System.Linq;

namespace GTASaveData.GTA3
{
    public class PhoneData : SaveDataObject,
        IEquatable<PhoneData>, IDeepClonable<PhoneData>
    {
        public const int MaxNumPhones = 50;

        private int m_numPhones;
        private int m_numActivePhones;
        private Array<Phone> m_phones;

        public int NumPhones
        {
            get { return m_numPhones; }
            set { m_numPhones = value; OnPropertyChanged(); }
        }

        public int NumActivePhones
        {
            get { return m_numActivePhones; }
            set { m_numActivePhones = value; OnPropertyChanged(); }
        }

        public Array<Phone> Phones
        {
            get { return m_phones; }
            set { m_phones = value; OnPropertyChanged(); }
        }

        public PhoneData()
        {
            Phones = ArrayHelper.CreateArray<Phone>(MaxNumPhones);
        }

        public PhoneData(PhoneData other)
        {
            NumPhones = other.NumPhones;
            NumActivePhones = other.NumActivePhones;
            Phones = ArrayHelper.DeepClone(other.Phones);
        }

        protected override void ReadData(StreamBuffer buf, FileFormat fmt)
        {
            NumPhones = buf.ReadInt32();
            NumActivePhones = buf.ReadInt32();
            Phones = buf.Read<Phone>(MaxNumPhones);

            Debug.Assert(buf.Offset == SizeOfType<PhoneData>());
        }

        protected override void WriteData(StreamBuffer buf, FileFormat fmt)
        {
            buf.Write(NumPhones);
            buf.Write(NumActivePhones);
            buf.Write(Phones.ToArray(), MaxNumPhones);

            Debug.Assert(buf.Offset == SizeOfType<PhoneData>());
        }

        protected override int GetSize(FileFormat fmt)
        {
            return 0xA30;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PhoneData);
        }

        public bool Equals(PhoneData other)
        {
            if (other == null)
            {
                return false;
            }

            return NumPhones.Equals(other.NumPhones)
                && NumActivePhones.Equals(other.NumActivePhones)
                && Phones.SequenceEqual(other.Phones);
        }

        public PhoneData DeepClone()
        {
            return new PhoneData(this);
        }
    }
}
