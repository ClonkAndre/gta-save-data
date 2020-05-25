﻿using GTASaveData.Types;
using System;
using System.Diagnostics;

namespace GTASaveData.GTA3
{
    public class RadarBlip : SaveDataObject, IEquatable<RadarBlip>
    {
        private int m_colorId;
        private RadarBlipType m_type;
        private int m_handle;
        private Vector2D m_radarPosition;
        private Vector3D m_worldPosition;
        private short m_blipIndex;
        private bool m_dim;
        private bool m_inUse;
        private float m_radius;
        private short m_scale;
        private RadarBlipDisplay m_display;
        private RadarBlipSprite m_sprite;

        public int ColorId
        {
            get { return m_colorId; }
            set { m_colorId = value; OnPropertyChanged(); }
        }

        public RadarBlipType Type
        {
            get { return m_type; }
            set { m_type = value; OnPropertyChanged(); }
        }

        public int Handle
        {
            get { return m_handle; }
            set { m_handle = value; OnPropertyChanged(); }
        }

        public Vector2D RadarPosition
        {
            get { return m_radarPosition; }
            set { m_radarPosition = value; OnPropertyChanged(); }
        }

        public Vector3D WorldPosition
        {
            get { return m_worldPosition; }
            set { m_worldPosition = value; OnPropertyChanged(); }
        }

        public short BlipIndex
        {
            get { return m_blipIndex; }
            set { m_blipIndex = value; OnPropertyChanged(); }
        }

        public bool Dim
        {
            get { return m_dim; }
            set { m_dim = value; OnPropertyChanged(); }
        }

        public bool InUse
        {
            get { return m_inUse; }
            set { m_inUse = value; OnPropertyChanged(); }
        }

        public float Radius
        {
            get { return m_radius; }
            set { m_radius = value; OnPropertyChanged(); }
        }

        public short Scale
        {
            get { return m_scale; }
            set { m_scale = value; OnPropertyChanged(); }
        }

        public RadarBlipDisplay Display
        {
            get { return m_display; }
            set { m_display = value; OnPropertyChanged(); }
        }

        public RadarBlipSprite Sprite
        {
            get { return m_sprite; }
            set { m_sprite = value; OnPropertyChanged(); }
        }

        public RadarBlip()
        {
            BlipIndex = 1;
        }

        protected override void ReadData(StreamBuffer buf, SaveDataFormat fmt)
        {
            ColorId = buf.ReadInt32();
            Type = (RadarBlipType) buf.ReadInt32();
            Handle = buf.ReadInt32();
            RadarPosition = buf.Read<Vector2D>();
            WorldPosition = buf.Read<Vector3D>();
            BlipIndex = buf.ReadInt16();
            Dim = buf.ReadBool();
            InUse = buf.ReadBool();
            Radius = buf.ReadFloat();
            Scale = buf.ReadInt16();
            Display = (RadarBlipDisplay) buf.ReadInt16();
            Sprite = (RadarBlipSprite) buf.ReadInt16();
            buf.ReadInt16();

            Debug.Assert(buf.Offset == SizeOf<RadarBlip>());
        }

        protected override void WriteData(StreamBuffer buf, SaveDataFormat fmt)
        {
            buf.Write(ColorId);
            buf.Write((int) Type);
            buf.Write(Handle);
            buf.Write(RadarPosition);
            buf.Write(WorldPosition);
            buf.Write(BlipIndex);
            buf.Write(Dim);
            buf.Write(InUse);
            buf.Write(Radius);
            buf.Write(Scale);
            buf.Write((short) Display);
            buf.Write((short) Sprite);
            buf.Write((short) 0);

            Debug.Assert(buf.Offset == SizeOf<RadarBlip>());
        }

        protected override int GetSize(SaveDataFormat fmt)
        {
            return 0x30;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RadarBlip);
        }

        public bool Equals(RadarBlip other)
        {
            if (other == null)
            {
                return false;
            }

            return ColorId.Equals(other.ColorId)
                && Type.Equals(other.Type)
                && Handle.Equals(other.Handle)
                && RadarPosition.Equals(other.RadarPosition)
                && WorldPosition.Equals(other.WorldPosition)
                && BlipIndex.Equals(other.BlipIndex)
                && Dim.Equals(other.Dim)
                && InUse.Equals(other.InUse)
                && Radius.Equals(other.Radius)
                && Scale.Equals(other.Scale)
                && Display.Equals(other.Display)
                && Sprite.Equals(other.Sprite);
        }
    }

    [Flags]
    public enum RadarBlipDisplay
    {
        None,
        Marker,
        Blip,
        MarkerAndBlip
    }

    public enum RadarBlipType
    {
        None,
        Car,
        Char,
        Object,
        Coord,
        ContactPoint
    }

    public enum RadarBlipSprite
    {
        None,
        Asuka,
        Bomb,
        Cat,
        Center,
        Copcar,
        Don,
        Eight,
        El,
        Ice,
        Joey,
        Kenji,
        Liz,
        Luigi,
        North,
        Ray,
        Sal,
        Save,
        Spray,
        Tony,
        Weapon
    }
}
