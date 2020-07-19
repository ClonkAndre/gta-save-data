﻿using GTASaveData.Types;
using System;
using System.Diagnostics;

namespace GTASaveData.LCS
{
    public class StoredCar : SaveDataObject,
        IEquatable<StoredCar>, IDeepClonable<StoredCar>
    {
        private int m_model;
        private Vector3D m_position;
        private Vector3D m_angle;
        private float m_handlingMultiplier;
        private StoredCarFlags m_flags;
        private byte m_color1;
        private byte m_color2;
        private RadioStation m_radio;
        private sbyte m_extra1;
        private sbyte m_extra2;

        public int Model
        {
            get { return m_model; }
            set { m_model = value; OnPropertyChanged(); }
        }

        public Vector3D Position
        {
            get { return m_position; }
            set { m_position = value; OnPropertyChanged(); }
        }

        public Vector3D Angle
        {
            get { return m_angle; }
            set { m_angle = value; OnPropertyChanged(); }
        }

        public float HandlingMultiplier
        {
            get { return m_handlingMultiplier; }
            set { m_handlingMultiplier = value; OnPropertyChanged(); }
        }

        public StoredCarFlags Flags
        {
            get { return m_flags; }
            set { m_flags = value; OnPropertyChanged(); }
        }

        public byte Color1
        {
            get { return m_color1; }
            set { m_color1 = value; OnPropertyChanged(); }
        }

        public byte Color2
        {
            get { return m_color2; }
            set { m_color2 = value; OnPropertyChanged(); }
        }

        public RadioStation Radio
        {
            get { return m_radio; }
            set { m_radio = value; OnPropertyChanged(); }
        }

        public sbyte Extra1
        {
            get { return m_extra1; }
            set { m_extra1 = value; OnPropertyChanged(); }
        }

        public sbyte Extra2
        {
            get { return m_extra2; }
            set { m_extra2 = value; OnPropertyChanged(); }
        }

        public StoredCar()
        { }

        public StoredCar(StoredCar other)
        {
            Model = other.Model;
            Position = other.Position;
            Angle = other.Angle;
            HandlingMultiplier = other.HandlingMultiplier;
            Flags = other.Flags;
            Color1 = other.Color1;
            Color2 = other.Color2;
            Radio = other.Radio;
            Extra1 = other.Extra1;
            Extra2 = other.Extra2;
        }

        protected override void ReadData(StreamBuffer buf, FileFormat fmt)
        {
            Model = buf.ReadInt32();
            Position = buf.Read<Vector3D>();
            Angle = buf.Read<Vector3D>();
            HandlingMultiplier = buf.ReadFloat();
            Flags = (StoredCarFlags) buf.ReadInt32();
            Color1 = buf.ReadByte();
            Color2 = buf.ReadByte();
            Radio = (RadioStation) buf.ReadSByte();
            Extra1 = buf.ReadSByte();
            Extra2 = buf.ReadSByte();
            buf.Skip(3);

            Debug.Assert(buf.Offset == SizeOfType<StoredCar>());
        }

        protected override void WriteData(StreamBuffer buf, FileFormat fmt)
        {
            buf.Write(Model);
            buf.Write(Position);
            buf.Write(Angle);
            buf.Write(HandlingMultiplier);
            buf.Write((int) Flags);
            buf.Write(Color1);
            buf.Write(Color2);
            buf.Write((sbyte) Radio);
            buf.Write(Extra1);
            buf.Write(Extra2);
            buf.Skip(3);

            Debug.Assert(buf.Offset == SizeOfType<StoredCar>());
        }

        protected override int GetSize(FileFormat fmt)
        {
            return 0x2C;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StoredCar);
        }

        public bool Equals(StoredCar other)
        {
            if (other == null)
            {
                return false;
            }

            return Model.Equals(other.Model)
                && Position.Equals(other.Position)
                && Angle.Equals(other.Angle)
                && HandlingMultiplier.Equals(other.HandlingMultiplier)
                && Flags.Equals(other.Flags)
                && Color1.Equals(other.Color1)
                && Color2.Equals(other.Color2)
                && Radio.Equals(other.Radio)
                && Extra1.Equals(other.Extra1)
                && Extra2.Equals(other.Extra2);
        }

        public StoredCar DeepClone()
        {
            return new StoredCar(this);
        }
    }

    [Flags]
    public enum StoredCarFlags
    {
        BulletProof     = 0b_00000000_00000001,
        FireProof       = 0b_00000000_00000010,
        ExplosionProof  = 0b_00000000_00000100,
        CollisionProof  = 0b_00000000_00001000,
        MeleeProof      = 0b_00000000_00010000,     // Doesn't work in-game
        PopProof        = 0b_00000000_00100000,
        Strong          = 0b_00000000_01000000,
        Heavy           = 0b_00000000_10000000,
        PermanentColor  = 0b_00000001_00000000,
        TimeBomb        = 0b_00000010_00000000,
        TipProof        = 0b_00000100_00000000,
        Unknown         = 0b_10000000_00000000,     // Doesn't do anything from what I can tell
    }

    public enum BombType
    {
        None,
        Timer,
        Ignition,
        Remote,
        TimerArmed,
        IgnitionArmed
    }

    public enum RadioStation
    {
        // TOOD: confirm
        HeadRadio,
        DoubleClefFM,
        KJah,
        RiseFM,
        Lips106,
        RadioDelMundo,
        Msx98,
        FlashbackFM,
        TheLibertyJam,
        LCFR,
        UserTrackPlayer,
        None
    }
}