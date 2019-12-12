﻿using GTASaveData.Common;
using System;

namespace GTASaveData.GTA3
{
    public sealed class SimpleVars : GTAObject
    {
        public const int SizeAndroid = 0xB0;
        public const int SizeIOS = 0xB0;
        public const int SizePC = 0xBC;
        public const int SizePS2 = 0xB0;
        public const int SizePS2AU = 0xA8;
        public const int SizePS2JP = 0xB0;
        public const int SizeXbox = 0xBC;

        private const int NameLength = 24;

        private const uint FileIdJP = 0x31400;
        private const uint FileIdNonJP = 0x31401;

        private string m_lastMissionPassedName;
        private uint m_fileId;
        private SystemTime m_saveTime;
        private Level m_currLevel;
        private Vector3d m_cameraPosition;
        private uint m_millisecondsPerGameMinute;
        private uint m_lastClockTick;
        private int m_gameClockHours;
        private int m_gameClockMinutes;
        private uint m_timeInMilliseconds;
        private float m_timeScale;
        private float m_timeScale2;
        private float m_timeStep;
        private float m_timeStep2;
        private float m_timeStepNonClipped;
        private float m_framesPerUpdate;
        private uint m_frameCounter;
        private Weather m_oldWeatherType;
        private Weather m_newWeatherType;
        private Weather m_forcedWeatherType;
        private float m_interpolationValue;
        private PadMode m_prefsControllerConfig;
        private int m_prefsMusicVolume;
        private int m_prefsSfxVolume;
        private bool m_prefsUseVibration;
        private bool m_prefsStereoMono;
        private Radio m_prefsRadioStation;
        private int m_prefsBrightness;
        private bool m_prefsUseWideScreen;
        private bool m_prefsShowTrails;
        private bool m_prefsShowSubtitles;
        private Language m_prefsLanguage;
        private Timestamp m_compileDateAndTime;
        private int m_weatherTypeInList;
        private float m_inCarCameraMode;
        private float m_onFootCameraMode;
        private bool m_isQuickSave;

        public string LastMissionPassedName
        {
            get { return m_lastMissionPassedName; }
            set { m_lastMissionPassedName = value; OnPropertyChanged(); }
        }

        public uint FileId
        {
            get { return m_fileId; }
            set { m_fileId = value; OnPropertyChanged(); }
        }

        public SystemTime SaveTime
        {
            get { return m_saveTime; }
            set { m_saveTime = value; OnPropertyChanged(); }
        }

        public Level CurrLevel
        {
            get { return m_currLevel; }
            set { m_currLevel = value; OnPropertyChanged(); }
        }

        public Vector3d CameraPosition
        {
            get { return m_cameraPosition; }
            set { m_cameraPosition = value; OnPropertyChanged(); }
        }

        public uint MillisecondsPerGameMinute
        {
            get { return m_millisecondsPerGameMinute; }
            set { m_millisecondsPerGameMinute = value; OnPropertyChanged(); }
        }

        public uint LastClockTick
        {
            get { return m_lastClockTick; }
            set { m_lastClockTick = value; OnPropertyChanged(); }
        }

        public int GameClockHours
        {
            get { return m_gameClockHours; }
            set { m_gameClockHours = value; OnPropertyChanged(); }
        }

        public int GameClockMinutes
        {
            get { return m_gameClockMinutes; }
            set { m_gameClockMinutes = value; OnPropertyChanged(); }
        }

        public uint TimeInMilliseconds
        {
            get { return m_timeInMilliseconds; }
            set { m_timeInMilliseconds = value; OnPropertyChanged(); }
        }

        public float TimeScale
        {
            get { return m_timeScale; }
            set { m_timeScale = value; OnPropertyChanged(); }
        }

        public float TimeScale2
        {
            get { return m_timeScale2; }
            set { m_timeScale2 = value; OnPropertyChanged(); }
        }

        public float TimeStep
        {
            get { return m_timeStep; }
            set { m_timeStep = value; OnPropertyChanged(); }
        }

        public float TimeStep2
        {
            get { return m_timeStep2; }
            set { m_timeStep2 = value; OnPropertyChanged(); }
        }

        public float TimeStepNonClipped
        {
            get { return m_timeStepNonClipped; }
            set { m_timeStepNonClipped = value; OnPropertyChanged(); }
        }

        public float FramesPerUpdate
        {
            get { return m_framesPerUpdate; }
            set { m_framesPerUpdate = value; OnPropertyChanged(); }
        }

        public uint FrameCounter
        {
            get { return m_frameCounter; }
            set { m_frameCounter = value; OnPropertyChanged(); }
        }

        public Weather OldWeatherType
        {
            get { return m_oldWeatherType; }
            set { m_oldWeatherType = value; OnPropertyChanged(); }
        }

        public Weather NewWeatherType
        {
            get { return m_newWeatherType; }
            set { m_newWeatherType = value; OnPropertyChanged(); }
        }

        public Weather ForcedWeatherType
        {
            get { return m_forcedWeatherType; }
            set { m_forcedWeatherType = value; OnPropertyChanged(); }
        }

        public float InterpolationValue
        {
            get { return m_interpolationValue; }
            set { m_interpolationValue = value; OnPropertyChanged(); }
        }

        public PadMode PrefsControllerConfig
        {
            get { return m_prefsControllerConfig; }
            set { m_prefsControllerConfig = value; OnPropertyChanged(); }
        }

        public int PrefsMusicVolume
        {
            get { return m_prefsMusicVolume; }
            set { m_prefsMusicVolume = value; OnPropertyChanged(); }
        }

        public int PrefsSfxVolume
        {
            get { return m_prefsSfxVolume; }
            set { m_prefsSfxVolume = value; OnPropertyChanged(); }
        }

        public bool PrefsUseVibration
        {
            get { return m_prefsUseVibration; }
            set { m_prefsUseVibration = value; OnPropertyChanged(); }
        }

        public bool PrefsStereoMono
        {
            get { return m_prefsStereoMono; }
            set { m_prefsStereoMono = value; OnPropertyChanged(); }
        }

        public Radio PrefsRadioStation
        {
            get { return m_prefsRadioStation; }
            set { m_prefsRadioStation = value; OnPropertyChanged(); }
        }

        public int PrefsBrightness
        {
            get { return m_prefsBrightness; }
            set { m_prefsBrightness = value; OnPropertyChanged(); }
        }

        public bool PrefsUseWideScreen
        {
            get { return m_prefsUseWideScreen; }
            set { m_prefsUseWideScreen = value; OnPropertyChanged(); }
        }

        public bool PrefsShowTrails
        {
            get { return m_prefsShowTrails; }
            set { m_prefsShowTrails = value; OnPropertyChanged(); }
        }

        public bool PrefsShowSubtitles
        {
            get { return m_prefsShowSubtitles; }
            set { m_prefsShowSubtitles = value; OnPropertyChanged(); }
        }

        public Language PrefsLanguage
        {
            get { return m_prefsLanguage; }
            set { m_prefsLanguage = value; OnPropertyChanged(); }
        }

        public Timestamp CompileDateAndTime
        {
            get { return m_compileDateAndTime; }
            set { m_compileDateAndTime = value; OnPropertyChanged(); }
        }

        public int WeatherTypeInList
        {
            get { return m_weatherTypeInList; }
            set { m_weatherTypeInList = value; OnPropertyChanged(); }
        }

        public float InCarCameraMode
        {
            get { return m_inCarCameraMode; }
            set { m_inCarCameraMode = value; OnPropertyChanged(); }
        }

        public float OnFootCameraMode
        {
            get { return m_onFootCameraMode; }
            set { m_onFootCameraMode = value; OnPropertyChanged(); }
        }

        public bool IsQuickSave
        {
            get { return m_isQuickSave; }
            set { m_isQuickSave = value; OnPropertyChanged(); }
        }

        public SimpleVars()
        {
            m_saveTime = new SystemTime();
            m_cameraPosition = new Vector3d();
            m_compileDateAndTime = new Timestamp();
        }

        private SimpleVars(Serializer serializer, SystemType system)
            : this()
        {
            if (!system.HasFlag(SystemType.PS2))
            {
                m_lastMissionPassedName = serializer.ReadString(NameLength, unicode: true);
                if (system.HasFlag(SystemType.PC) || system.HasFlag(SystemType.Xbox))
                {
                    m_saveTime = serializer.ReadObject<SystemTime>();
                }
            }
            m_fileId = serializer.ReadUInt32();
            m_currLevel = (Level) serializer.ReadUInt32();
            m_cameraPosition = serializer.ReadObject<Vector3d>();
            m_millisecondsPerGameMinute = serializer.ReadUInt32();
            m_lastClockTick = serializer.ReadUInt32();
            if (system.HasFlag(SystemType.PS2))
            {
                m_gameClockHours = serializer.ReadInt32();
                m_gameClockMinutes = serializer.ReadInt32();
                m_prefsControllerConfig = (PadMode) serializer.ReadInt32();
            }
            else
            {
                m_gameClockHours = serializer.ReadByte();
                serializer.Align();
                m_gameClockMinutes = serializer.ReadByte();
                serializer.Align();
                m_prefsControllerConfig = (PadMode) serializer.ReadInt16();
                serializer.Align();
            }
            m_timeInMilliseconds = serializer.ReadUInt32();
            m_timeScale = serializer.ReadSingle();
            m_timeStep = serializer.ReadSingle();
            m_timeStepNonClipped = serializer.ReadSingle();
            m_frameCounter = serializer.ReadUInt32();
            m_timeStep2 = serializer.ReadSingle();
            m_framesPerUpdate = serializer.ReadSingle();
            m_timeScale2 = serializer.ReadSingle();
            m_oldWeatherType = (Weather) serializer.ReadInt16();
            serializer.Align();
            m_newWeatherType = (Weather) serializer.ReadInt16();
            serializer.Align();
            m_forcedWeatherType = (Weather) serializer.ReadInt16();
            serializer.Align();
            m_interpolationValue = serializer.ReadSingle();
            if (system.HasFlag(SystemType.PS2))
            {
                m_prefsMusicVolume = serializer.ReadInt32();
                m_prefsSfxVolume = serializer.ReadInt32();
                if (!system.HasFlag(SystemType.Australian))
                {
                    m_prefsControllerConfig = (PadMode) serializer.ReadInt32();
                }
                m_prefsUseVibration = serializer.ReadBool(4);
                m_prefsStereoMono = serializer.ReadBool(4);
                m_prefsRadioStation = (Radio) serializer.ReadInt32();
                m_prefsBrightness = serializer.ReadInt32();
                if (!system.HasFlag(SystemType.Australian))
                {
                    m_prefsShowTrails = serializer.ReadBool(4);
                }
                m_prefsShowSubtitles = serializer.ReadBool(4);
                m_prefsLanguage = (Language) serializer.ReadInt32();
                m_prefsUseWideScreen = serializer.ReadBool(4);
                m_prefsControllerConfig = (PadMode) serializer.ReadInt32();
                m_prefsShowTrails = serializer.ReadBool(4);
            }
            m_compileDateAndTime = serializer.ReadObject<Timestamp>();
            m_weatherTypeInList = serializer.ReadInt32();
            m_inCarCameraMode = serializer.ReadSingle();
            m_onFootCameraMode = serializer.ReadSingle();
            if (system.HasFlag(SystemType.Android) || system.HasFlag(SystemType.IOS))
            {
                m_isQuickSave = serializer.ReadBool(4);
            }
        }

        private void Serialize(Serializer serializer, SystemType system)
        {
            if (!system.HasFlag(SystemType.PS2))
            {
                serializer.Write(m_lastMissionPassedName, NameLength, unicode: true);
                if (system.HasFlag(SystemType.PC) || system.HasFlag(SystemType.Xbox))
                {
                    serializer.WriteObject(m_saveTime);
                }
            }
            serializer.Write(system.HasFlag(SystemType.PS2JP) ? FileIdJP : FileIdNonJP);
            serializer.Write((int) m_currLevel);
            serializer.WriteObject(m_cameraPosition);
            serializer.Write(m_millisecondsPerGameMinute);
            serializer.Write(m_lastClockTick);
            if (system.HasFlag(SystemType.PS2))
            {
                serializer.Write(m_gameClockHours);
                serializer.Write(m_gameClockMinutes);
                serializer.Write((int) m_prefsControllerConfig);
            }
            else
            {
                serializer.Write((byte) m_gameClockHours);
                serializer.Align();
                serializer.Write((byte) m_gameClockMinutes);
                serializer.Align();
                serializer.Write((short) m_prefsControllerConfig);
                serializer.Align();
            }
            serializer.Write(m_timeInMilliseconds);
            serializer.Write(m_timeScale);
            serializer.Write(m_timeStep);
            serializer.Write(m_timeStepNonClipped);
            serializer.Write(m_frameCounter);
            serializer.Write(m_timeStep2);
            serializer.Write(m_framesPerUpdate);
            serializer.Write(m_timeScale2);
            serializer.Write((short) m_oldWeatherType);
            serializer.Align();
            serializer.Write((short) m_newWeatherType);
            serializer.Align();
            serializer.Write((short) m_forcedWeatherType);
            serializer.Align();
            serializer.Write(m_interpolationValue);
            if (system.HasFlag(SystemType.PS2))
            {
                serializer.Write(m_prefsMusicVolume);
                serializer.Write(m_prefsSfxVolume);
                if (!system.HasFlag(SystemType.Australian))
                {
                    serializer.Write((int) m_prefsControllerConfig);
                }
                serializer.Write(m_prefsUseVibration, 4);
                serializer.Write(m_prefsStereoMono, 4);
                serializer.Write((int) m_prefsRadioStation);
                serializer.Write(m_prefsBrightness);
                if (!system.HasFlag(SystemType.Australian))
                {
                    serializer.Write(m_prefsShowTrails, 4);
                }
                serializer.Write(m_prefsShowSubtitles, 4);
                serializer.Write((int) m_prefsLanguage);
                serializer.Write(m_prefsUseWideScreen, 4);
                serializer.Write((int) m_prefsControllerConfig);
                serializer.Write(m_prefsShowTrails, 4);
            }
            serializer.WriteObject(m_compileDateAndTime);
            serializer.Write(m_weatherTypeInList);
            serializer.Write(m_inCarCameraMode);
            serializer.Write(m_onFootCameraMode);
            if (system.HasFlag(SystemType.Android) || system.HasFlag(SystemType.IOS))
            {
                serializer.Write(m_isQuickSave, 4);
            }
        }

        public static int GetSize(SystemType sys)
        {
            switch (sys)
            {
                case SystemType.Android: return SizeAndroid;
                case SystemType.IOS:     return SizeIOS;
                case SystemType.PC:      return SizePC;
                case SystemType.PS2:     return SizePS2;
                case SystemType.PS2AU:   return SizePS2AU;
                case SystemType.PS2JP:   return SizePS2JP;
                case SystemType.Xbox:    return SizeXbox;
                default:
                    throw new ArgumentException("Invalid system type.", nameof(sys));
            }
        }

        // TODO: euqality, tostring
    }
}
