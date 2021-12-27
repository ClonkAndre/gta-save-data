﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using GTASaveData.Interfaces;

namespace GTASaveData.GTA3
{
    public class PlayerPed : SaveDataObject,
        IEquatable<PlayerPed>, IDeepClonable<PlayerPed>
    {
        public const int NumWeapons = 13;
        public const int MaxModelNameLength = 24;
        public const int NumTargetableObjects = 4;

        private PedTypeId m_type;
        private short m_modelIndex;
        private int m_handle;
        private Vector3 m_position;
        private CharCreatedBy m_createdBy;
        private float m_health;
        private float m_armor;
        private ObservableArray<Weapon> m_weapons;
        private byte m_maxWeaponTypeAllowed;
        private float m_maxStamina;
        private ObservableArray<int> m_targetableObjects;
        private int m_maxWantedLevel;
        private int m_maxChaosLevel;
        private string m_modelName;

        public PedTypeId Type
        {
            get { return m_type; }
            set { m_type = value; OnPropertyChanged(); }
        }

        public short ModelIndex
        {
            get { return m_modelIndex; }
            set { m_modelIndex = value; OnPropertyChanged(); }
        }

        public int Handle
        {
            get { return m_handle; }
            set { m_handle = value; OnPropertyChanged(); }
        }

        public Vector3 Position
        {
            get { return m_position; }
            set { m_position = value; OnPropertyChanged(); }
        }

        public CharCreatedBy CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; OnPropertyChanged(); }
        }

        public float Health
        {
            get { return m_health; }
            set { m_health = value; OnPropertyChanged(); }
        }

        public float Armor
        {
            get { return m_armor; }
            set { m_armor = value; OnPropertyChanged(); }
        }

        public ObservableArray<Weapon> Weapons
        {
            get { return m_weapons; }
            set { m_weapons = value; OnPropertyChanged(); }
        }

        public byte MaxWeaponTypeAllowed
        {
            get { return m_maxWeaponTypeAllowed; }
            set { m_maxWeaponTypeAllowed = value; OnPropertyChanged(); }
        }

        public float MaxStamina
        {
            get { return m_maxStamina; }
            set { m_maxStamina = value; OnPropertyChanged(); }
        }

        public ObservableArray<int> TargetableObjects
        {
            get { return m_targetableObjects; }
            set { m_targetableObjects = value; OnPropertyChanged(); }
        }


        public int MaxWantedLevel
        {
            get { return m_maxWantedLevel; }
            set { m_maxWantedLevel = value; OnPropertyChanged(); }
        }

        public int MaxChaosLevel
        {
            get { return m_maxChaosLevel; }
            set { m_maxChaosLevel = value; OnPropertyChanged(); }
        }

        public string ModelName
        {
            get { return m_modelName; }
            set { m_modelName = value; OnPropertyChanged(); }
        }

        public PlayerPed()
            : this(0, (1 << 8) & 1)
        { }

        public PlayerPed(short model, int handle)
        {
            ModelIndex = model;
            Handle = handle;
            Type = PedTypeId.Player1;
            CreatedBy = CharCreatedBy.Mission;
            ModelName = "player";
            Health = 100;
            MaxStamina = 150;
            Weapons = ArrayHelper.CreateArray<Weapon>(NumWeapons);
            TargetableObjects = ArrayHelper.CreateArray<int>(NumTargetableObjects);
            Position = new Vector3();
        }

        public PlayerPed(PlayerPed other)
        {
            Type = other.Type;
            ModelIndex = other.ModelIndex;
            Handle = other.Handle;
            Position = other.Position;
            CreatedBy = other.CreatedBy;
            Health = other.Health;
            Armor = other.Armor;
            Weapons = ArrayHelper.DeepClone(other.Weapons);
            MaxWeaponTypeAllowed = other.MaxWeaponTypeAllowed;
            MaxStamina = other.MaxStamina;
            TargetableObjects = ArrayHelper.DeepClone(other.TargetableObjects);
            MaxWantedLevel = other.MaxWantedLevel;
            MaxChaosLevel = other.MaxChaosLevel;
            ModelName = other.ModelName;
        }

        protected override void ReadData(DataBuffer buf, SerializationParams prm)
        {
            var t = prm.FileType;

            if (!t.IsPS2) buf.Skip(4);
            buf.Skip(48);
            Position = buf.ReadStruct<Vector3>();
            if(t.IsPC || t.IsXbox) buf.Skip(288);
            if (t.IsiOS) buf.Skip(292);
            if (t.IsAndroid) buf.Skip(296);
            if (t.IsPS2 && t.FlagJapan) buf.Skip(324);
            else if (t.IsPS2) buf.Skip(356);
            CreatedBy = (CharCreatedBy) buf.ReadByte();
            buf.Skip(351);
            if (t.IsXbox) buf.Skip(4);
            Health = buf.ReadFloat();
            Armor = buf.ReadFloat();
            if (t.IsPS2) buf.Skip(164);
            else buf.Skip(148);
            Weapons = buf.ReadArray<Weapon>(NumWeapons, prm);
            buf.Skip(5);
            MaxWeaponTypeAllowed = buf.ReadByte();
            buf.Skip(178);
            if (t.IsMobile) buf.Skip(4);
            if (t.IsPS2) buf.Skip(8);
            MaxStamina = buf.ReadFloat();
            buf.Skip(28);
            TargetableObjects = buf.ReadArray<int>(NumTargetableObjects);
            if (t.IsPC || t.IsXbox) buf.Skip(116);
            if (t.IsMobile) buf.Skip(144);
            if (t.IsPS2) buf.Skip(16);

            Debug.Assert(buf.Offset == SizeOf<PlayerPed>(prm));
        }

        protected override void WriteData(DataBuffer buf, SerializationParams prm)
        {
            var t = prm.FileType;

            if (!t.IsPS2) buf.Skip(4);
            buf.Skip(48);
            buf.Write(Position);
            if (t.IsPC || t.IsXbox) buf.Skip(288);
            if (t.IsiOS) buf.Skip(292);
            if (t.IsAndroid) buf.Skip(296);
            if (t.IsPS2 && t.FlagJapan) buf.Skip(324);
            else if (t.IsPS2) buf.Skip(356);
            buf.Write((byte) CreatedBy);
            buf.Skip(351);
            if (t.IsXbox) buf.Skip(4);
            buf.Write(Health);
            buf.Write(Armor);
            if (t.IsPS2) buf.Skip(164);
            else buf.Skip(148);
            buf.Write(Weapons, prm, NumWeapons);
            buf.Skip(5);
            buf.Write(MaxWeaponTypeAllowed);
            buf.Skip(178);
            if (t.IsMobile) buf.Skip(4);
            if (t.IsPS2) buf.Skip(8);
            buf.Write(MaxStamina);
            buf.Skip(28);
            buf.Write(TargetableObjects, NumTargetableObjects);
            if (t.IsPC || t.IsXbox) buf.Skip(116);
            if (t.IsMobile) buf.Skip(144);
            if (t.IsPS2) buf.Skip(16);

            Debug.Assert(buf.Offset == SizeOf<PlayerPed>(prm));
        }

        protected override int GetSize(SerializationParams prm)
        {
            var t = prm.FileType;
            if (t.IsPS2 && t.FlagJapan) return 0x590;
            if (t.IsPS2) return 0x5B0;
            if (t.IsPC) return 0x5F0;
            if (t.IsXbox) return 0x5F4;
            if (t.IsiOS) return 0x614;
            if (t.IsAndroid) return 0x618;
            throw SizeNotDefined(t);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlayerPed);
        }

        public bool Equals(PlayerPed other)
        {
            if (other == null)
            {
                return false;
            }

            return Type.Equals(other.Type)
                && ModelIndex.Equals(other.ModelIndex)
                && Handle.Equals(other.Handle)
                && Position.Equals(other.Position)
                && CreatedBy.Equals(other.CreatedBy)
                && Health.Equals(other.Health)
                && Armor.Equals(other.Armor)
                && Weapons.SequenceEqual(other.Weapons)
                && MaxWeaponTypeAllowed.Equals(other.MaxWeaponTypeAllowed)
                && MaxStamina.Equals(other.MaxStamina)
                && TargetableObjects.SequenceEqual(other.TargetableObjects)
                && MaxWantedLevel.Equals(other.MaxWantedLevel)
                && MaxChaosLevel.Equals(other.MaxChaosLevel)
                && ModelName.Equals(other.ModelName);
        }

        public PlayerPed DeepClone()
        {
            return new PlayerPed(this);
        }
    }

    public enum CharCreatedBy
    {
        Unknown,
        Random,
        Mission
    }
}