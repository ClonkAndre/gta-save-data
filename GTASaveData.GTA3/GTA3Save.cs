using GTASaveData.Extensions;
using GTASaveData.Types;
using GTASaveData.Types.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GTASaveData.GTA3
{
    /// <summary>
    /// Represents a <i>Grand Theft Auto III</i> save file.
    /// </summary>
    public class GTA3Save : GTA3VCSave, IDisposable, IGTASaveFile, IEquatable<GTA3Save>
    {
        public const int SizeOfOneGameInBytes = 201729;
        public const int MaxBufferSize = 55000;

        private bool m_disposed;

        protected override int BufferSize => (FileFormat.PS2) ? 50000 : 55000;

        private SimpleVariables m_simpleVars;
        private ScriptData m_scripts;
        private PedPool m_pedPool;
        private GarageData m_garages;
        private VehiclePool m_vehiclePool;
        private ObjectPool m_objectPool;
        private PathData m_paths;
        private CraneData m_cranes;
        private PickupData m_pickups;
        private PhoneData m_phoneInfo;
        private RestartData m_restartPoints;
        private RadarData m_radarBlips;
        private ZoneData m_zones;
        private GangData m_gangs;
        private CarGeneratorData m_carGenerators;
        private ParticleData m_particleObjects;
        private AudioScriptData m_audioScriptObjects;
        private PlayerInfo m_playerInfo;
        private Stats m_stats;
        private Streaming m_streaming;
        private PedTypeData m_pedType;

        public SimpleVariables SimpleVars
        {
            get { return m_simpleVars; }
            set { m_simpleVars = value; OnPropertyChanged(); }
        }

        public ScriptData Scripts
        {
            get { return m_scripts; }
            set { m_scripts = value; OnPropertyChanged(); }
        }

        public PedPool PedPool
        {
            get { return m_pedPool; }
            set { m_pedPool = value; OnPropertyChanged(); }
        }

        public GarageData Garages
        {
            get { return m_garages; }
            set { m_garages = value; OnPropertyChanged(); }
        }

        public VehiclePool VehiclePool
        {
            get { return m_vehiclePool; }
            set { m_vehiclePool = value; OnPropertyChanged(); }
        }

        public ObjectPool ObjectPool
        {
            get { return m_objectPool; }
            set { m_objectPool = value; OnPropertyChanged(); }
        }

        public PathData Paths
        {
            get { return m_paths; }
            set { m_paths = value; OnPropertyChanged(); }
        }

        public CraneData Cranes
        {
            get { return m_cranes; }
            set { m_cranes = value; OnPropertyChanged(); }
        }

        public PickupData Pickups
        {
            get { return m_pickups; }
            set { m_pickups = value; OnPropertyChanged(); }
        }

        public PhoneData PhoneInfo
        {
            get { return m_phoneInfo; }
            set { m_phoneInfo = value; OnPropertyChanged(); }
        }

        public RestartData RestartPoints
        {
            get { return m_restartPoints; }
            set { m_restartPoints = value; OnPropertyChanged(); }
        }

        public RadarData RadarBlips
        {
            get { return m_radarBlips; }
            set { m_radarBlips = value; OnPropertyChanged(); }
        }

        public ZoneData Zones
        {
            get { return m_zones; }
            set { m_zones = value; OnPropertyChanged(); }
        }

        public GangData Gangs
        {
            get { return m_gangs; }
            set { m_gangs = value; OnPropertyChanged(); }
        }

        public CarGeneratorData CarGenerators
        {
            get { return m_carGenerators; }
            set { m_carGenerators = value; OnPropertyChanged(); }
        }

        public ParticleData ParticleObjects
        {
            get { return m_particleObjects; }
            set { m_particleObjects = value; OnPropertyChanged(); }
        }

        public AudioScriptData AudioScriptObjects
        {
            get { return m_audioScriptObjects; }
            set { m_audioScriptObjects = value; OnPropertyChanged(); }
        }

        public PlayerInfo PlayerInfo
        {
            get { return m_playerInfo; }
            set { m_playerInfo = value; OnPropertyChanged(); }
        }

        public Stats Stats
        {
            get { return m_stats; }
            set { m_stats = value; OnPropertyChanged(); }
        }

        public Streaming Streaming
        {
            get { return m_streaming; }
            set { m_streaming = value; OnPropertyChanged(); }
        }

        public PedTypeData PedTypeInfo
        {
            get { return m_pedType; }
            set { m_pedType = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public override string Name
        {
            get { return SimpleVars.SaveName; }
            set { SimpleVars.SaveName = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public override DateTime TimeLastSaved
        {
            get { return (DateTime) SimpleVars.TimeLastSaved; }
            set { SimpleVars.TimeLastSaved = new SystemTime(value); OnPropertyChanged(); }
        }

        [JsonIgnore]
        public override IReadOnlyList<SaveDataObject> Blocks => new List<SaveDataObject>()
        {
            SimpleVars,
            Scripts,
            PedPool,
            Garages,
            VehiclePool,
            ObjectPool,
            Paths,
            Cranes,
            Pickups,
            PhoneInfo,
            RestartPoints,
            RadarBlips,
            Zones,
            Gangs,
            CarGenerators,
            ParticleObjects,
            AudioScriptObjects,
            PlayerInfo,
            Stats,
            Streaming,
            PedTypeInfo
        };

        public GTA3Save() : base(MaxBufferSize)
        {
            m_disposed = false;

            SimpleVars = new SimpleVariables();
            Scripts = new ScriptData();
            PedPool = new PedPool();
            Garages = new GarageData();
            VehiclePool = new VehiclePool();
            ObjectPool = new ObjectPool();
            Paths = new PathData();
            Cranes = new CraneData();
            Pickups = new PickupData();
            PhoneInfo = new PhoneData();
            RestartPoints = new RestartData();
            RadarBlips = new RadarData();
            Zones = new ZoneData();
            Gangs = new GangData();
            CarGenerators = new CarGeneratorData();
            ParticleObjects = new ParticleData();
            AudioScriptObjects = new AudioScriptData();
            PlayerInfo = new PlayerInfo();
            Stats = new Stats();
            Streaming = new Streaming();
            PedTypeInfo = new PedTypeData();

        #if !DEBUG
            BlockSizeChecks = true;
        #endif
        }

        protected override void LoadSimpleVars()
        {
            SimpleVars = WorkBuff.Read<SimpleVariables>(FileFormat);
        }

        protected override void SaveSimpleVars()
        {
            SimpleVars.SaveSize = SizeOfOneGameInBytes;
            WorkBuff.Write(SimpleVars, FileFormat);
        }

        protected override void LoadAllData(StreamBuffer file)
        {
            int totalSize = 0;

            totalSize += ReadBlock(file);
            LoadSimpleVars();                
            Scripts = Load<ScriptData>();
            totalSize += ReadBlock(file); PedPool = Load<PedPool>();
            totalSize += ReadBlock(file); Garages = Load<GarageData>();
            totalSize += ReadBlock(file); VehiclePool = Load<VehiclePool>();
            totalSize += ReadBlock(file); ObjectPool = Load<ObjectPool>();
            totalSize += ReadBlock(file); Paths = LoadPreAlloc<PathData>();
            totalSize += ReadBlock(file); Cranes = Load<CraneData>();
            totalSize += ReadBlock(file); Pickups = Load<PickupData>();
            totalSize += ReadBlock(file); PhoneInfo = Load<PhoneData>();
            totalSize += ReadBlock(file); RestartPoints = Load<RestartData>();
            totalSize += ReadBlock(file); RadarBlips = Load<RadarData>();
            totalSize += ReadBlock(file); Zones = Load<ZoneData>();
            totalSize += ReadBlock(file); Gangs = Load<GangData>();
            totalSize += ReadBlock(file); CarGenerators = Load<CarGeneratorData>();
            totalSize += ReadBlock(file); ParticleObjects = Load<ParticleData>();
            totalSize += ReadBlock(file); AudioScriptObjects = Load<AudioScriptData>();
            totalSize += ReadBlock(file); PlayerInfo = Load<PlayerInfo>();
            totalSize += ReadBlock(file); Stats = Load<Stats>();
            totalSize += ReadBlock(file); Streaming = Load<Streaming>();
            totalSize += ReadBlock(file); PedTypeInfo = Load<PedTypeData>();

            while (file.Cursor < file.Length - 4)
            {
                totalSize += ReadBlock(file);
            }

            Debug.WriteLine("Load successful!");
            Debug.WriteLine("Size of game data: {0} bytes", totalSize);
            Debug.Assert(totalSize == (SizeOfOneGameInBytes & 0xFFFFFFFE));
        }

        protected override void SaveAllData(StreamBuffer file)
        {
            int totalSize = 0;
            int size;

            WorkBuff.Reset();
            CheckSum = 0;

            SaveSimpleVars();
            Save(Scripts); totalSize += WriteBlock(file);
            Save(PedPool); totalSize += WriteBlock(file);
            Save(Garages); totalSize += WriteBlock(file);
            Save(VehiclePool); totalSize += WriteBlock(file);
            Save(ObjectPool); totalSize += WriteBlock(file);
            Save(Paths); totalSize += WriteBlock(file);
            Save(Cranes); totalSize += WriteBlock(file);
            Save(Pickups); totalSize += WriteBlock(file);
            Save(PhoneInfo); totalSize += WriteBlock(file);
            Save(RestartPoints); totalSize += WriteBlock(file);
            Save(RadarBlips); totalSize += WriteBlock(file);
            Save(Zones); totalSize += WriteBlock(file);
            Save(Gangs); totalSize += WriteBlock(file);
            Save(CarGenerators); totalSize += WriteBlock(file);
            Save(ParticleObjects); totalSize += WriteBlock(file);
            Save(AudioScriptObjects); totalSize += WriteBlock(file);
            Save(PlayerInfo); totalSize += WriteBlock(file);
            Save(Stats); totalSize += WriteBlock(file);
            Save(Streaming); totalSize += WriteBlock(file);
            Save(PedTypeInfo); totalSize += WriteBlock(file);

            for (int i = 0; i < 4; i++)
            {
                size = StreamBuffer.Align4Bytes(SizeOfOneGameInBytes - totalSize - 4);
                if (size > BufferSize)
                {
                    size = BufferSize;
                }
                if (size > 4)
                {
                    if (Padding != PaddingType.Default)
                    {
                        WorkBuff.Reset();
                        WorkBuff.Write(GenerateSpecialPadding(size));
                    }
                    WorkBuff.Seek(size);
                    totalSize += WriteBlock(file);
                }
            }

            file.Write(CheckSum);

            Debug.WriteLine("Save successful!");
            Debug.WriteLine("Size of game data: {0} bytes", totalSize);
            Debug.Assert(totalSize == (SizeOfOneGameInBytes & 0xFFFFFFFE));
        }

        protected override bool DetectFileFormat(byte[] data, out SaveDataFormat fmt)
        {
            bool isMobile = false;
            bool isPcOrXbox = false;

            int fileId = data.FindFirst(BitConverter.GetBytes(0x31401));
            int fileIdJP = data.FindFirst(BitConverter.GetBytes(0x31400));
            int scr = data.FindFirst("SCR\0".GetAsciiBytes());

            if (fileId == -1 || scr == -1)
            {
                fmt = SaveDataFormat.Default;
                return false;
            }

            int blk1Size;   // TODO: use another metric as block 1 can change sizes in rare circumstances
            using (StreamBuffer wb = new StreamBuffer(data))
            {
                wb.Skip(wb.ReadInt32());
                blk1Size = wb.ReadInt32();
            }

            if (scr == 0xB0 && fileId == 0x04)
            {
                fmt = FileFormats.PS2_AU;
                return true;
            }
            else if (scr == 0xB8)
            {
                if (fileIdJP == 0x04)
                {
                    fmt = FileFormats.PS2_JP;
                    return true;
                }
                else if (fileId == 0x04)
                {
                    fmt = FileFormats.PS2_NAEU;
                    return true;
                }
                else if (fileId == 0x34)
                {
                    isMobile = true;
                }
            }
            else if (scr == 0xC4 && fileId == 0x44)
            {
                isPcOrXbox = true;
            }

            if (isMobile)
            {
                if (blk1Size == 0x648)
                {
                    fmt = FileFormats.iOS;
                    return true;
                }
                else if (blk1Size == 0x64C)
                {
                    fmt = FileFormats.Android;
                    return true;
                }
            }
            else if (isPcOrXbox)
            {
                if (blk1Size == 0x624)
                {
                    fmt = FileFormats.PC;
                    return true;
                }
                else if (blk1Size == 0x628)
                {
                    fmt = FileFormats.Xbox;
                    return true;
                }
            }

            fmt = SaveDataFormat.Default;
            return false;
        }

        protected override int GetSize(SaveDataFormat fmt)
        {
            // TODO:
            throw SizeNotDefined(fmt);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GTA3Save);
        }

        public bool Equals(GTA3Save other)
        {
            if (other == null)
            {
                return false;
            }

            return SimpleVars.Equals(other.SimpleVars)
                && Scripts.Equals(other.Scripts)
                && PedPool.Equals(other.PedPool)
                && Garages.Equals(other.Garages)
                && VehiclePool.Equals(other.VehiclePool)
                && ObjectPool.Equals(other.ObjectPool)
                && Paths.Equals(other.Paths)
                && Cranes.Equals(other.Cranes)
                && Pickups.Equals(other.Pickups)
                && PhoneInfo.Equals(other.PhoneInfo)
                && RestartPoints.Equals(other.RestartPoints)
                && RadarBlips.Equals(other.RadarBlips)
                && Zones.Equals(other.Zones)
                && Gangs.Equals(other.Gangs)
                && CarGenerators.Equals(other.CarGenerators)
                && ParticleObjects.Equals(other.ParticleObjects)
                && AudioScriptObjects.Equals(other.AudioScriptObjects)
                && PlayerInfo.Equals(other.PlayerInfo)
                && Stats.Equals(other.Stats)
                && Streaming.Equals(other.Streaming)
                && PedTypeInfo.Equals(other.PedTypeInfo);
        }

        public void Dispose()
        {
            if (!m_disposed)
            {
                WorkBuff.Dispose();
                m_disposed = true;
            }
        }

        public static class FileFormats
        {
            public static readonly SaveDataFormat Android = new SaveDataFormat(
                "Android", "Android", "Android",
                new GameConsole(ConsoleType.Android)
            );

            public static readonly SaveDataFormat iOS = new SaveDataFormat(
                "iOS", "iOS", "iOS",
                new GameConsole(ConsoleType.iOS)
            );

            public static readonly SaveDataFormat PC = new SaveDataFormat(
                "PC", "PC", "Windows, macOS",
                new GameConsole(ConsoleType.Win32),
                new GameConsole(ConsoleType.MacOS)
            );

            public static readonly SaveDataFormat PS2_AU = new SaveDataFormat(
                "PS2_AU", "PS2 (Australia)", "PlayStation 2 (PAL Australia)",
                new GameConsole(ConsoleType.PS2, ConsoleFlags.Australia)
            );

            public static readonly SaveDataFormat PS2_JP = new SaveDataFormat(
                "PS2_JP", "PS2 (Japan)", "PlayStation 2 (NTSC-J)",
                new GameConsole(ConsoleType.PS2, ConsoleFlags.Japan)
            );

            public static readonly SaveDataFormat PS2_NAEU = new SaveDataFormat(
                "PS2_NAEU", "PS2", "PlayStation 2 (NTSC-U/C, PAL Europe)",
                new GameConsole(ConsoleType.PS2, ConsoleFlags.NorthAmerica | ConsoleFlags.Europe)
            );

            public static readonly SaveDataFormat Xbox = new SaveDataFormat(
                "Xbox", "Xbox", "Xbox",
                new GameConsole(ConsoleType.Xbox)
            );

            public static SaveDataFormat[] GetAll()
            {
                return new SaveDataFormat[] { Android, iOS, PC, PS2_AU, PS2_JP, PS2_NAEU, Xbox };
            }
        }

        public static bool IsAusrtalianPS2(SaveDataFormat fmt)
        {
            return fmt.IsSupportedOn(ConsoleType.PS2, ConsoleFlags.Australia);
        }

        public static bool IsJapanesePS2(SaveDataFormat fmt)
        {
            return fmt.IsSupportedOn(ConsoleType.PS2, ConsoleFlags.Japan);
        }
    }

    public enum DataBlock
    {
        SimpleVars,
        Scripts,
        PedPool,
        Garages,
        VehiclePool,
        ObjectPool,
        PathFind,
        Cranes,
        Pickups,
        PhoneInfo,
        RestartPoints,
        RadarBlips,
        Zones,
        Gangs,
        CarGenerators,
        ParticleObjects,
        AudioScriptObjects,
        PlayerInfo,
        Stats,
        Streaming,
        PedTypeInfo
    }
}
