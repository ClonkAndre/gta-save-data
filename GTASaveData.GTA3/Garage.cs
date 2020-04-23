﻿using GTASaveData.Types;
using System;
using System.Diagnostics;

#pragma warning disable CS0618 // Type or member is obsolete
namespace GTASaveData.GTA3
{
    [Size(0x8C)]
    public class Garage : SaveDataObject, IEquatable<Garage>
    {
        private GarageType m_type;
        private GarageState m_state;
        private bool m_field02h;    // not used by game
        private bool m_closingWithoutTargetCar;
        private bool m_deactivated;
        private bool m_resprayHappened;
        private int m_targetModelIndex;
        private uint m_pDoor1;      // zeroed on load
        private uint m_pDoor2;      // zeroed on load
        private byte m_door1Handle;
        private byte m_door2Handle;
        private bool m_isDoor1Dummy;
        private bool m_isDoor2Dummy;
        private bool m_recreateDoorOnNextRefresh;   // set to true on load
        private bool m_rotatedDoor;
        private bool m_cameraFollowsPlayer;
        private Vector m_posInf;
        private Vector m_posSup;
        private float m_doorOpenOffset;
        private float m_doorOpenMax;
        private Vector m_door1Pos;
        private Vector m_door2Pos;
        private uint m_timeToStartAction;
        private byte m_collectedCarsState;
        private uint m_pTargetCar;  // zeroed on load
        private int m_field96h; // not used by game
        private StoredCar m_storedCar;

        public GarageType Type
        {
            get { return m_type; }
            set { m_type = value; OnPropertyChanged(); }
        }

        public GarageState State
        {
            get { return m_state; }
            set { m_state = value; OnPropertyChanged(); }
        }

        [Obsolete("Not used by the game.")]
        public bool Field02h
        {
            get { return m_field02h; }
            set { m_field02h = value; OnPropertyChanged(); }
        }

        public bool ClosingWithoutTargetVehicle
        {
            get { return m_closingWithoutTargetCar; }
            set { m_closingWithoutTargetCar = value; OnPropertyChanged(); }
        }

        public bool Deactivated
        {
            get { return m_deactivated; }
            set { m_deactivated = value; OnPropertyChanged(); }
        }

        public bool ResprayHappened
        {
            get { return m_resprayHappened; }
            set { m_resprayHappened = value; OnPropertyChanged(); }
        }

        public int TargetModelIndex
        {
            get { return m_targetModelIndex; }
            set { m_targetModelIndex = value; OnPropertyChanged(); }
        }

        [Obsolete("Value overridden by the game.")]
        public uint Door1Pointer
        {
            get { return m_pDoor1; }
            set { m_pDoor1 = value; OnPropertyChanged(); }
        }

        [Obsolete("Value overridden by the game.")]
        public uint Door2Pointer
        {
            get { return m_pDoor2; }
            set { m_pDoor2 = value; OnPropertyChanged(); }
        }

        public byte Door1Handle
        {
            get { return m_door1Handle; }
            set { m_door1Handle = value; OnPropertyChanged(); }
        }

        public byte Door2Handle
        {
            get { return m_door2Handle; }
            set { m_door2Handle = value; OnPropertyChanged(); }
        }

        public bool IsDoor1Dummy
        {
            get { return m_isDoor1Dummy; }
            set { m_isDoor1Dummy = value; OnPropertyChanged(); }
        }

        public bool IsDoor2Dummy
        {
            get { return m_isDoor2Dummy; }
            set { m_isDoor2Dummy = value; OnPropertyChanged(); }
        }

        [Obsolete("Value overridden by the game.")]
        public bool RecreateDoorOnNextRefresh
        {
            get { return m_recreateDoorOnNextRefresh; }
            set { m_recreateDoorOnNextRefresh = value; OnPropertyChanged(); }
        }

        public bool RotatedDoor
        {
            get { return m_rotatedDoor; }
            set { m_rotatedDoor = value; OnPropertyChanged(); }
        }

        public bool CameraFollowsPlayer
        {
            get { return m_cameraFollowsPlayer; }
            set { m_cameraFollowsPlayer = value; OnPropertyChanged(); }
        }

        public Vector PositionMin
        {
            get { return m_posInf; }
            set { m_posInf = value; OnPropertyChanged(); }
        }

        public Vector PositionMax
        {
            get { return m_posSup; }
            set { m_posSup = value; OnPropertyChanged(); }
        }

        public float DoorOpenOffset
        {
            get { return m_doorOpenOffset; }
            set { m_doorOpenOffset = value; OnPropertyChanged(); }
        }

        public float DoorOpenMax
        {
            get { return m_doorOpenMax; }
            set { m_doorOpenMax = value; OnPropertyChanged(); }
        }

        public Vector Door1Position
        {
            get { return m_door1Pos; }
            set { m_door1Pos = value; OnPropertyChanged(); }
        }

        public Vector Door2Position
        {
            get { return m_door2Pos; }
            set { m_door2Pos = value; OnPropertyChanged(); }
        }

        public uint DoorLastOpenTime
        {
            get { return m_timeToStartAction; }
            set { m_timeToStartAction = value; OnPropertyChanged(); }
        }

        public byte CollectedCarsState
        {
            get { return m_collectedCarsState; }
            set { m_collectedCarsState = value; OnPropertyChanged(); }
        }

        [Obsolete("Value overridden by the game.")]
        public uint TargetCarPointer
        {
            get { return m_pTargetCar; }
            set { m_pTargetCar = value; OnPropertyChanged(); }
        }

        [Obsolete("Not used by the game.")]
        public int Field96h
        {
            get { return m_field96h; }
            set { m_field96h = value; OnPropertyChanged(); }
        }

        public StoredCar StoredCar
        {
            get { return m_storedCar; }
            set { m_storedCar = value; OnPropertyChanged(); }
        }

        public Garage()
        {
            PositionMin = new Vector();
            PositionMax = new Vector();
            Door1Position = new Vector();
            Door2Position = new Vector();
            StoredCar = new StoredCar();
        }

        protected override void ReadObjectData(StreamBuffer buf, DataFormat fmt)
        {
            Type = (GarageType) buf.ReadByte();
            State = (GarageState) buf.ReadByte();
            Field02h = buf.ReadBool();
            ClosingWithoutTargetVehicle = buf.ReadBool();
            Deactivated = buf.ReadBool();
            ResprayHappened = buf.ReadBool();
            buf.Align4Bytes();
            TargetModelIndex = buf.ReadInt32();
            Door1Pointer = buf.ReadUInt32();
            Door2Pointer = buf.ReadUInt32();
            Door1Handle = buf.ReadByte();
            Door2Handle = buf.ReadByte();
            IsDoor1Dummy = buf.ReadBool();
            IsDoor2Dummy = buf.ReadBool();
            RecreateDoorOnNextRefresh = buf.ReadBool();
            RotatedDoor = buf.ReadBool();
            CameraFollowsPlayer = buf.ReadBool();
            buf.Align4Bytes();
            PositionMin.X = buf.ReadFloat();
            PositionMax.X = buf.ReadFloat();
            PositionMin.Y = buf.ReadFloat();
            PositionMax.Y = buf.ReadFloat();
            PositionMin.Z = buf.ReadFloat();
            PositionMax.Z = buf.ReadFloat();
            DoorOpenOffset = buf.ReadFloat();
            DoorOpenMax = buf.ReadFloat();
            Door1Position.X = buf.ReadFloat();
            Door1Position.Y = buf.ReadFloat();
            Door2Position.X = buf.ReadFloat();
            Door2Position.Y = buf.ReadFloat();
            Door1Position.Z = buf.ReadFloat();
            Door2Position.Z = buf.ReadFloat();
            DoorLastOpenTime = buf.ReadUInt32();
            CollectedCarsState = buf.ReadByte();
            buf.Align4Bytes();
            TargetCarPointer = buf.ReadUInt32();
            Field96h = buf.ReadInt32();
            StoredCar = buf.Read<StoredCar>();

            Debug.Assert(buf.Offset == SizeOf<Garage>());
        }

        protected override void WriteObjectData(StreamBuffer buf, DataFormat fmt)
        {
            buf.Write((byte) Type);
            buf.Write((byte) State);
            buf.Write(Field02h);
            buf.Write(ClosingWithoutTargetVehicle);
            buf.Write(Deactivated);
            buf.Write(ResprayHappened);
            buf.Align4Bytes();
            buf.Write(TargetModelIndex);
            buf.Write(Door1Pointer);
            buf.Write(Door2Pointer);
            buf.Write(Door1Handle);
            buf.Write(Door2Handle);
            buf.Write(IsDoor1Dummy);
            buf.Write(IsDoor2Dummy);
            buf.Write(RecreateDoorOnNextRefresh);
            buf.Write(RotatedDoor);
            buf.Write(CameraFollowsPlayer);
            buf.Align4Bytes();
            buf.Write(PositionMin.X);
            buf.Write(PositionMax.X);
            buf.Write(PositionMin.Y);
            buf.Write(PositionMax.Y);
            buf.Write(PositionMin.Z);
            buf.Write(PositionMax.Z);
            buf.Write(DoorOpenOffset);
            buf.Write(DoorOpenMax);
            buf.Write(Door1Position.X);
            buf.Write(Door1Position.Y);
            buf.Write(Door2Position.X);
            buf.Write(Door2Position.Y);
            buf.Write(Door1Position.Z);
            buf.Write(Door2Position.Z);
            buf.Write(DoorLastOpenTime);
            buf.Write(CollectedCarsState);
            buf.Align4Bytes();
            buf.Write(TargetCarPointer);
            buf.Write(Field96h);
            buf.Write(StoredCar);

            Debug.Assert(buf.Offset == SizeOf<Garage>());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Garage);
        }

        public bool Equals(Garage other)
        {
            if (other == null)
            {
                return false;
            }

            return Type.Equals(other.Type)
                && State.Equals(other.State)
                && Field02h.Equals(other.Field02h)
                && ClosingWithoutTargetVehicle.Equals(other.ClosingWithoutTargetVehicle)
                && Deactivated.Equals(other.Deactivated)
                && ResprayHappened.Equals(other.ResprayHappened)
                && TargetModelIndex.Equals(other.TargetModelIndex)
                && Door1Pointer.Equals(other.Door1Pointer)
                && Door2Pointer.Equals(other.Door2Pointer)
                && Door1Handle.Equals(other.Door1Handle)
                && Door2Handle.Equals(other.Door2Handle)
                && IsDoor1Dummy.Equals(other.IsDoor1Dummy)
                && IsDoor2Dummy.Equals(other.IsDoor2Dummy)
                && RecreateDoorOnNextRefresh.Equals(other.RecreateDoorOnNextRefresh)
                && RotatedDoor.Equals(other.RotatedDoor)
                && CameraFollowsPlayer.Equals(other.CameraFollowsPlayer)
                && PositionMin.Equals(other.PositionMin)
                && PositionMax.Equals(other.PositionMax)
                && DoorOpenOffset.Equals(other.DoorOpenOffset)
                && DoorOpenMax.Equals(other.DoorOpenMax)
                && Door1Position.Equals(other.Door1Position)
                && Door2Position.Equals(other.Door2Position)
                && DoorLastOpenTime.Equals(other.DoorLastOpenTime)
                && CollectedCarsState.Equals(other.CollectedCarsState)
                && TargetCarPointer.Equals(other.TargetCarPointer)
                && Field96h.Equals(other.Field96h)
                && StoredCar.Equals(other.StoredCar);
        }
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
