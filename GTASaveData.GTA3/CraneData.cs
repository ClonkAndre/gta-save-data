﻿using System;
using System.Diagnostics;
using System.Linq;

namespace GTASaveData.GTA3
{
    public class CraneData : SaveDataObject,
        IEquatable<CraneData>, IDeepClonable<CraneData>
    {
        public const int MaxNumCranes = 8;

        private int m_numCranes;
        private CollectCarsMilitaryCrane m_carsCollectedMilitaryCrane;
        private Array<Crane> m_cranes;

        public int NumCranes
        {
            get { return m_numCranes; }
            set { m_numCranes = value; }
        }

        public CollectCarsMilitaryCrane CarsCollectedMilitaryCrane
        {
            get { return m_carsCollectedMilitaryCrane; }
            set { m_carsCollectedMilitaryCrane = value; OnPropertyChanged(); }
        }

        public Array<Crane> Cranes
        {
            get { return m_cranes; }
            set { m_cranes = value; OnPropertyChanged(); }
        }

        public CraneData()
        {
            Cranes = ArrayHelper.CreateArray<Crane>(MaxNumCranes);
        }

        public CraneData(CraneData other)
        {
            Cranes = ArrayHelper.DeepClone(other.Cranes);
        }

        protected override void ReadData(StreamBuffer buf, FileFormat fmt)
        {
            NumCranes = buf.ReadInt32();
            CarsCollectedMilitaryCrane = (CollectCarsMilitaryCrane) buf.ReadInt32();
            Cranes = buf.Read<Crane>(MaxNumCranes);

            Debug.Assert(buf.Offset == SizeOfType<CraneData>());
        }

        protected override void WriteData(StreamBuffer buf, FileFormat fmt)
        {
            buf.Write(NumCranes);
            buf.Write((int) CarsCollectedMilitaryCrane);
            buf.Write(Cranes.ToArray(), MaxNumCranes);

            Debug.Assert(buf.Offset == SizeOfType<CraneData>());
        }

        protected override int GetSize(FileFormat fmt)
        {
            return 0x408;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CraneData);
        }

        public bool Equals(CraneData other)
        {
            if (other == null)
            {
                return false;
            }

            return NumCranes.Equals(other.NumCranes)
                && CarsCollectedMilitaryCrane.Equals(other.CarsCollectedMilitaryCrane)
                && Cranes.SequenceEqual(other.Cranes);
        }

        public CraneData DeepClone()
        {
            return new CraneData(this);
        }
    }

    public enum CraneState
    {
        Idle,
        GoingTowardsTarget,
        LiftingTarget,
        GoingTowardsTargetOnlyHeight,
        RotatingTarget,
        DroppingTarget
    }

    public enum CraneStatus
    {
        None,
        Activated,
        Deactivated
    }

    [Flags]
    public enum CollectCarsMilitaryCrane
    {
        Firetruck = (1 << 0),
        Ambulance = (1 << 1),
        Enforcer = (1 << 2),
        FbiCar = (1 << 3),
        Rhino = (1 << 4),
        BarracksOL = (1 << 5),
        Police = (1 << 6),
    }
}
