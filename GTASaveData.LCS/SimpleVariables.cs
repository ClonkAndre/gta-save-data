﻿using GTASaveData.Types;
using System;

namespace GTASaveData.LCS
{
    public class SimpleVariables : SaveDataObject,
        IEquatable<SimpleVariables>, IDeepClonable<SimpleVariables>
    {
        public const int MaxMissionPassedNameLength = 60;

        private string m_lastMissionPassedName;
        private int m_currentLevel;
        private int m_currentArea;
        private int m_prefsLanguage;
        private int m_millisecondsPerGameMinute;
        private uint m_lastClockTick;
        private byte m_gameClockHours;
        private byte m_gameClockMinutes;
        private short m_gameClockSeconds;
        private uint m_timeInMilliseconds;
        private float m_timeScale;
        private float m_timeStep;
        private float m_timeStepNonClipped;
        private float m_framesPerUpdate;
        private uint m_frameCounter;
        private WeatherType m_oldWeatherType;
        private WeatherType m_newWeatherType;
        private WeatherType m_forcedWeatherType;
        private int m_weatherTypeInList;
        private float m_weatherInterpolationValue;
        private Vector3D m_cameraPosition;
        private float m_cameraModeInCar;
        private float m_cameraModeOnFoot;
        private int m_timeCyc0;
        private int m_timeCyc1;
        private float m_timeCyc2;
        private int m_prefsBrightness;
        private bool m_prefsDisplayHud;
        private bool m_prefsShowSubtitles;
        private RadarMode m_prefsRadarMode;
        private bool m_blurOn;  // turns on PSP color filter
        private int m_prefsMusicVolume;
        private int m_prefsSfxVolume;
        private RadioStation m_prefsRadioStation;
        private bool m_prefsStereoMono;
        private short m_padMode;
        private bool m_prefsInvertLook;     // TOOD: value is inverted on PS2/PSP
        private bool m_prefsUseVibration;
        private bool m_swapNippleAndDPad;
        private bool m_hasPlayerCheated;
        private bool m_allTaxisHaveNitro;
        private bool m_targetIsOn;
        private Vector2D m_targetPosition;
        private Date m_timeStamp;

        public string LastMissionPassedName
        {
            get { return m_lastMissionPassedName; }
            set { m_lastMissionPassedName = value; OnPropertyChanged(); }
        }

        public int CurrentLevel
        {
            get { return m_currentLevel; }
            set { m_currentLevel = value; OnPropertyChanged(); }
        }

        public int CurrentArea
        {
            get { return m_currentArea; }
            set { m_currentArea = value; OnPropertyChanged(); }
        }

        public int Language
        {
            get { return m_prefsLanguage; }
            set { m_prefsLanguage = value; OnPropertyChanged(); }
        }

        public int MillisecondsPerGameMinute
        {
            get { return m_millisecondsPerGameMinute; }
            set { m_millisecondsPerGameMinute = value; OnPropertyChanged(); }
        }

        public uint LastClockTick
        {
            get { return m_lastClockTick; }
            set { m_lastClockTick = value; OnPropertyChanged(); }
        }

        public byte GameClockHours
        {
            get { return m_gameClockHours; }
            set { m_gameClockHours = value; OnPropertyChanged(); }
        }

        public byte GameClockMinutes
        {
            get { return m_gameClockMinutes; }
            set { m_gameClockMinutes = value; OnPropertyChanged(); }
        }

        public short GameClockSeconds
        {
            get { return m_gameClockSeconds; }
            set { m_gameClockSeconds = value; OnPropertyChanged(); }
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

        public float TimeStep
        {
            get { return m_timeStep; }
            set { m_timeStep = value; OnPropertyChanged(); }
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

        public WeatherType OldWeatherType
        {
            get { return m_oldWeatherType; }
            set { m_oldWeatherType = value; OnPropertyChanged(); }
        }

        public WeatherType NewWeatherType
        {
            get { return m_newWeatherType; }
            set { m_newWeatherType = value; OnPropertyChanged(); }
        }

        public WeatherType ForcedWeatherType
        {
            get { return m_forcedWeatherType; }
            set { m_forcedWeatherType = value; OnPropertyChanged(); }
        }

        public int WeatherTypeInList
        {
            get { return m_weatherTypeInList; }
            set { m_weatherTypeInList = value; OnPropertyChanged(); }
        }

        public float WeatherInterpolationValue
        {
            get { return m_weatherInterpolationValue; }
            set { m_weatherInterpolationValue = value; OnPropertyChanged(); }
        }

        public Vector3D CameraPosition
        {
            get { return m_cameraPosition; }
            set { m_cameraPosition = value; OnPropertyChanged(); }
        }

        public float CameraModeInCar
        {
            get { return m_cameraModeInCar; }
            set { m_cameraModeInCar = value; OnPropertyChanged(); }
        }

        public float CameraModeOnFoot
        {
            get { return m_cameraModeOnFoot; }
            set { m_cameraModeOnFoot = value; OnPropertyChanged(); }
        }

        public int TimeCycleValue0
        {
            get { return m_timeCyc0; }
            set { m_timeCyc0 = value; OnPropertyChanged(); }
        }

        public int TimeCycleValue1
        {
            get { return m_timeCyc1; }
            set { m_timeCyc1 = value; OnPropertyChanged(); }
        }

        public float TimeCycleValue2
        {
            get { return m_timeCyc2; }
            set { m_timeCyc2 = value; OnPropertyChanged(); }
        }

        public int Brightness
        {
            get { return m_prefsBrightness; }
            set { m_prefsBrightness = value; OnPropertyChanged(); }
        }

        public bool DisplayHud
        {
            get { return m_prefsDisplayHud; }
            set { m_prefsDisplayHud = value; OnPropertyChanged(); }
        }

        public bool ShowSubtitles
        {
            get { return m_prefsShowSubtitles; }
            set { m_prefsShowSubtitles = value; OnPropertyChanged(); }
        }

        public RadarMode RadarMode
        {
            get { return m_prefsRadarMode; }
            set { m_prefsRadarMode = value; OnPropertyChanged(); }
        }

        public bool BlurOn
        {
            get { return m_blurOn; }
            set { m_blurOn = value; OnPropertyChanged(); }
        }

        public int MusicVolume
        {
            get { return m_prefsMusicVolume; }
            set { m_prefsMusicVolume = value; OnPropertyChanged(); }
        }

        public int SfxVolume
        {
            get { return m_prefsSfxVolume; }
            set { m_prefsSfxVolume = value; OnPropertyChanged(); }
        }

        public RadioStation RadioStation
        {
            get { return m_prefsRadioStation; }
            set { m_prefsRadioStation = value; OnPropertyChanged(); }
        }

        public bool StereoOutput
        {
            get { return m_prefsStereoMono; }
            set { m_prefsStereoMono = value; OnPropertyChanged(); }
        }

        public short PadMode
        {
            get { return m_padMode; }
            set { m_padMode = value; OnPropertyChanged(); }
        }

        public bool InvertLook
        {
            get { return m_prefsInvertLook; }
            set { m_prefsInvertLook = value; OnPropertyChanged(); }
        }

        public bool UseVibration
        {
            get { return m_prefsUseVibration; }
            set { m_prefsUseVibration = value; OnPropertyChanged(); }
        }

        public bool SwapNippleAndDPad
        {
            get { return m_swapNippleAndDPad; }
            set { m_swapNippleAndDPad = value; OnPropertyChanged(); }
        }

        public bool HasPlayerCheated
        {
            get { return m_hasPlayerCheated; }
            set { m_hasPlayerCheated = value; OnPropertyChanged(); }
        }

        public bool AllTaxisHaveNitro
        {
            get { return m_allTaxisHaveNitro; }
            set { m_allTaxisHaveNitro = value; OnPropertyChanged(); }
        }

        public bool TargetIsOn
        {
            get { return m_targetIsOn; }
            set { m_targetIsOn = value; OnPropertyChanged(); }
        }

        public Vector2D TargetPosition
        {
            get { return m_targetPosition; }
            set { m_targetPosition = value; OnPropertyChanged(); }
        }

        public Date TimeStamp
        {
            get { return m_timeStamp; }
            set { m_timeStamp = value; OnPropertyChanged(); }
        }

        public SimpleVariables()
        {
            LastMissionPassedName = "";
            TimeStamp = DateTime.Now;
        }

        public SimpleVariables(SimpleVariables other)
        {
            LastMissionPassedName = other.LastMissionPassedName;
            CurrentLevel = other.CurrentLevel;
            CurrentArea = other.CurrentArea;
            Language = other.Language;
            MillisecondsPerGameMinute = other.MillisecondsPerGameMinute;
            LastClockTick = other.LastClockTick;
            GameClockHours = other.GameClockHours;
            GameClockMinutes = other.GameClockMinutes;
            GameClockSeconds = other.GameClockSeconds;
            TimeInMilliseconds = other.TimeInMilliseconds;
            TimeScale = other.TimeScale;
            TimeStep = other.TimeStep;
            TimeStepNonClipped = other.TimeStepNonClipped;
            FramesPerUpdate = other.FramesPerUpdate;
            FrameCounter = other.FrameCounter;
            OldWeatherType = other.OldWeatherType;
            NewWeatherType = other.NewWeatherType;
            ForcedWeatherType = other.ForcedWeatherType;
            WeatherTypeInList = other.WeatherTypeInList;
            WeatherInterpolationValue = other.WeatherInterpolationValue;
            CameraPosition = other.CameraPosition;
            CameraModeInCar = other.CameraModeInCar;
            CameraModeOnFoot = other.CameraModeOnFoot;
            TimeCycleValue0 = other.TimeCycleValue0;
            TimeCycleValue1 = other.TimeCycleValue1;
            TimeCycleValue2 = other.TimeCycleValue2;
            Brightness = other.Brightness;
            DisplayHud = other.DisplayHud;
            ShowSubtitles = other.ShowSubtitles;
            RadarMode = other.RadarMode;
            BlurOn = other.BlurOn;
            MusicVolume = other.MusicVolume;
            SfxVolume = other.SfxVolume;
            RadioStation = other.RadioStation;
            StereoOutput = other.StereoOutput;
            PadMode = other.PadMode;
            InvertLook = other.InvertLook;
            UseVibration = other.UseVibration;
            SwapNippleAndDPad = other.SwapNippleAndDPad;
            HasPlayerCheated = other.HasPlayerCheated;
            AllTaxisHaveNitro = other.AllTaxisHaveNitro;
            TargetIsOn = other.TargetIsOn;
            TargetPosition = other.TargetPosition;
            TimeStamp = other.TimeStamp;
        }

        protected override void ReadData(StreamBuffer buf, FileFormat fmt)
        {
            //throw new NotImplementedException();
        }

        protected override void WriteData(StreamBuffer buf, FileFormat fmt)
        {
            //throw new NotImplementedException();
        }

        protected override int GetSize(FileFormat fmt)
        {
            if (fmt.IsPSP) return 0xBC;
            if (fmt.IsPS2) return 0xF8;
            return 0x13C;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SimpleVariables);
        }

        public bool Equals(SimpleVariables other)
        {
            if (other == null)
            {
                return false;
            }

            return LastMissionPassedName.Equals(other.LastMissionPassedName)
                && CurrentLevel.Equals(other.CurrentLevel)
                && CurrentArea.Equals(other.CurrentArea)
                && Language.Equals(other.Language)
                && MillisecondsPerGameMinute.Equals(other.MillisecondsPerGameMinute)
                && LastClockTick.Equals(other.LastClockTick)
                && GameClockHours.Equals(other.GameClockHours)
                && GameClockMinutes.Equals(other.GameClockMinutes)
                && GameClockSeconds.Equals(other.GameClockSeconds)
                && TimeInMilliseconds.Equals(other.TimeInMilliseconds)
                && TimeScale.Equals(other.TimeScale)
                && TimeStep.Equals(other.TimeStep)
                && TimeStepNonClipped.Equals(other.TimeStepNonClipped)
                && FramesPerUpdate.Equals(other.FramesPerUpdate)
                && FrameCounter.Equals(other.FrameCounter)
                && OldWeatherType.Equals(other.OldWeatherType)
                && NewWeatherType.Equals(other.NewWeatherType)
                && ForcedWeatherType.Equals(other.ForcedWeatherType)
                && WeatherTypeInList.Equals(other.WeatherTypeInList)
                && WeatherInterpolationValue.Equals(other.WeatherInterpolationValue)
                && CameraPosition.Equals(other.CameraPosition)
                && CameraModeInCar.Equals(other.CameraModeInCar)
                && CameraModeOnFoot.Equals(other.CameraModeOnFoot)
                && TimeCycleValue0.Equals(other.TimeCycleValue0)
                && TimeCycleValue1.Equals(other.TimeCycleValue1)
                && TimeCycleValue2.Equals(other.TimeCycleValue2)
                && Brightness.Equals(other.Brightness)
                && DisplayHud.Equals(other.DisplayHud)
                && ShowSubtitles.Equals(other.ShowSubtitles)
                && RadarMode.Equals(other.RadarMode)
                && BlurOn.Equals(other.BlurOn)
                && MusicVolume.Equals(other.MusicVolume)
                && SfxVolume.Equals(other.SfxVolume)
                && RadioStation.Equals(other.RadioStation)
                && StereoOutput.Equals(other.StereoOutput)
                && PadMode.Equals(other.PadMode)
                && InvertLook.Equals(other.InvertLook)
                && UseVibration.Equals(other.UseVibration)
                && SwapNippleAndDPad.Equals(other.SwapNippleAndDPad)
                && HasPlayerCheated.Equals(other.HasPlayerCheated)
                && AllTaxisHaveNitro.Equals(other.AllTaxisHaveNitro)
                && TargetIsOn.Equals(other.TargetIsOn)
                && TargetPosition.Equals(other.TargetPosition)
                && TimeStamp.Equals(other.TimeStamp);
        }

        public SimpleVariables DeepClone()
        {
            return new SimpleVariables(this);
        }
    }

    public enum WeatherType
    {
        Sunny,
        Cloudy,
        Rainy,
        Foggy,
        ExtraSunny,
        Hurricane,
        ExtraColours,
        Snowy       // PSP only :(
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

    public enum RadarMode
    {
        // TOOD: confirm
        MapAndBlips,
        BlipsOnly,
        RadarOff,
    }
}


//LastMissionPassedName
//CurrentLevel
//CurrentArea
//Language
//MillisecondsPerGameMinute
//LastClockTick
//GameClockHours
//GameClockMinutes
//GameClockSeconds
//TimeInMilliseconds
//TimeScale
//TimeStep
//TimeStepNonClipped
//FramesPerUpdate
//FrameCounter
//OldWeatherType
//NewWeatherType
//ForcedWeatherType
//WeatherTypeInList
//WeatherInterpolationValue
//CameraPosition
//CameraModeInCar
//CameraModeOnFoot
//TimeCycleValue0
//TimeCycleValue1
//TimeCycleValue2
//Brightness
//DisplayHud
//ShowSubtitles
//RadarMode
//BlurOn
//MusicVolume
//SfxVolume
//RadioStation
//StereoOutput
//PadMode
//InvertLook
//UseVibration
//SwapNippleAndDPad
//HasPlayerCheated
//AllTaxisHaveNitro
//TargetIsOn
//TargetPosition
//TimeStamp