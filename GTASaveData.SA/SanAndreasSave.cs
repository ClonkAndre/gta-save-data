﻿using GTASaveData.Extensions;
using GTASaveData.Types;
using GTASaveData.Types.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GTASaveData.SA
{
    /// <summary>
    /// Represents a <i>Grand Theft Auto: San Andreas</i> save file.
    /// </summary>
    public class SanAndreasSave : SaveFile, ISaveFile, IEquatable<SanAndreasSave>, IDisposable
    {
        public const int SizeOfOneGameInBytes = 202752;
        private const int MaxBufferSize = 65000;
        private const int BlockHeaderSize = 5;
        private const int BlockCount = 28;
        private const int BlockCountMobile = 29;
        private const string BlockTagName = "BLOCK";

        private readonly StreamBuffer m_workBuffer;
        private int m_bufferSize => (FileFormat.IsSupportedOnMobile) ? 65000 : 51200;
        private int m_checkSum;
        private bool m_disposed;

        private StreamBuffer m_file;
        private SimpleVariables m_simpleVars;
        private Dummy m_scripts;      // TheScripts
        private Dummy m_pools;   // Pools
        private Dummy m_garages;   // Garages
        private Dummy m_gameLogic;   // GameLogic
        private Dummy m_paths;   // PathFind
        private Dummy m_pickups;   // Pickups
        private Dummy m_block7;   // empty
        private Dummy m_restartPoints;   // Restart
        private Dummy m_radarBlips;   // Radar
        private Dummy m_zones;   // TheZones
        private Dummy m_gangData;   // Gangs
        private Dummy m_carGenerators;   // TheCarGenerators
        private Dummy m_block13;   // empty
        private Dummy m_block14;   // empty
        private Dummy m_playerInfo;   // PlayerInfo
        private Dummy m_stats;   // Stats
        private Dummy m_setPieces;   // SetPieces
        private Dummy m_streaming;   // Streaming
        private Dummy m_pedTypeInfo;   // PedType
        private Dummy m_tags;   // TagManager
        private Dummy m_iplStore;   // IplStore
        private Dummy m_shopping;   // Shopping
        private Dummy m_gangWars;   // GangWars
        private Dummy m_stuntJumps;   // StuntJumpManager
        private Dummy m_entryExits;   // EntryExitManager
        private Dummy m_radio;   // AERadioTrackManager
        private Dummy m_user3dMarkers;   // User3dMarkers
        private Dummy m_postEffects;   // PostEffects (Mobile only)

        public SimpleVariables SimpleVars
        {
            get { return m_simpleVars; }
            set { m_simpleVars = value; }
        }

        public Dummy Scripts
        {
            get { return m_scripts; }
            set { m_scripts = value; }
        }

        public Dummy Pools
        {
            get { return m_pools; }
            set { m_pools = value; }
        }

        public Dummy Garages
        {
            get { return m_garages; }
            set { m_garages = value; }
        }

        public Dummy GameLogic
        {
            get { return m_gameLogic; }
            set { m_gameLogic = value; }
        }

        public Dummy Paths
        {
            get { return m_paths; }
            set { m_paths = value; }
        }

        public Dummy Pickups
        {
            get { return m_pickups; }
            set { m_pickups = value; }
        }

        public Dummy PhoneInfo
        {
            get { return m_block7; }
            set { m_block7 = value; }
        }

        public Dummy RestartPoints
        {
            get { return m_restartPoints; }
            set { m_restartPoints = value; }
        }

        public Dummy RadarBlips
        {
            get { return m_radarBlips; }
            set { m_radarBlips = value; }
        }

        public Dummy Zones
        {
            get { return m_zones; }
            set { m_zones = value; }
        }

        public Dummy GangData
        {
            get { return m_gangData; }
            set { m_gangData = value; }
        }

        public Dummy CarGenerators
        {
            get { return m_carGenerators; }
            set { m_carGenerators = value; }
        }

        public Dummy PedGenerators
        {
            get { return m_block13; }
            set { m_block13 = value; }
        }

        public Dummy AudioScriptObjects
        {
            get { return m_block14; }
            set { m_block14 = value; }
        }

        public Dummy PlayerInfo
        {
            get { return m_playerInfo; }
            set { m_playerInfo = value; }
        }

        public Dummy Stats
        {
            get { return m_stats; }
            set { m_stats = value; }
        }

        public Dummy SetPieces
        {
            get { return m_setPieces; }
            set { m_setPieces = value; }
        }

        public Dummy Streaming
        {
            get { return m_streaming; }
            set { m_streaming = value; }
        }

        public Dummy PedTypeInfo
        {
            get { return m_pedTypeInfo; }
            set { m_pedTypeInfo = value; }
        }

        public Dummy Tags
        {
            get { return m_tags; }
            set { m_tags = value; }
        }

        public Dummy IplStore
        {
            get { return m_iplStore; }
            set { m_iplStore = value; }
        }

        public Dummy Shopping
        {
            get { return m_shopping; }
            set { m_shopping = value; }
        }

        public Dummy GangWars
        {
            get { return m_gangWars; }
            set { m_gangWars = value; }
        }

        public Dummy StuntJumps
        {
            get { return m_stuntJumps; }
            set { m_stuntJumps = value; }
        }

        public Dummy EntryExits
        {
            get { return m_entryExits; }
            set { m_entryExits = value; }
        }

        public Dummy Radio
        {
            get { return m_radio; }
            set { m_radio = value; }
        }

        public Dummy User3dMarkers
        {
            get { return m_user3dMarkers; }
            set { m_user3dMarkers = value; }
        }

        public Dummy PostEffects
        {
            get { return m_postEffects; }
            set { m_postEffects = value; }
        }

        [JsonIgnore]
        public override string Name
        {
            get { return SimpleVars.LastMissionPassedName; }
            set { SimpleVars.LastMissionPassedName = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public override DateTime TimeLastSaved
        {
            get { return SimpleVars.TimeLastSaved; }
            set { SimpleVars.TimeLastSaved = new SystemTime(value); OnPropertyChanged(); }
        }

        [JsonIgnore]
        public override IReadOnlyList<SaveDataObject> Blocks => new List<SaveDataObject>()
        {
            SimpleVars,
            Scripts,
            Pools,
            Garages,
            GameLogic,
            Paths,
            Pickups,
            PhoneInfo,
            RestartPoints,
            RadarBlips,
            Zones,
            GangData,
            CarGenerators,
            PedGenerators,
            AudioScriptObjects,
            PlayerInfo,
            Stats,
            SetPieces,
            Streaming,
            PedTypeInfo,
            Tags,
            IplStore,
            Shopping,
            GangWars,
            StuntJumps,
            EntryExits,
            Radio,
            User3dMarkers,
            PostEffects
        };

        public SanAndreasSave()
        {
            m_disposed = false;
            m_workBuffer = new StreamBuffer(new byte[MaxBufferSize]);

            SimpleVars = new SimpleVariables();
            Scripts = new Dummy();
            Pools = new Dummy();
            Garages = new Dummy();
            GameLogic = new Dummy();
            Paths = new Dummy();
            Pickups = new Dummy();
            PhoneInfo = new Dummy();
            RestartPoints = new Dummy();
            RadarBlips = new Dummy();
            Zones = new Dummy();
            GangData = new Dummy();
            CarGenerators = new Dummy();
            PedGenerators = new Dummy();
            AudioScriptObjects = new Dummy();
            PlayerInfo = new Dummy();
            Stats = new Dummy();
            SetPieces = new Dummy();
            Streaming = new Dummy();
            PedTypeInfo = new Dummy();
            Tags = new Dummy();
            IplStore = new Dummy();
            Shopping = new Dummy();
            GangWars = new Dummy();
            StuntJumps = new Dummy();
            EntryExits = new Dummy();
            Radio = new Dummy();
            User3dMarkers = new Dummy(0x8C);
            PostEffects = new Dummy(0x160);
        }

        private int LoadBlockHeader()
        {
            int count = LoadDataFromWorkBuffer(5, out byte[] tagBytes);
            string tag = Encoding.ASCII.GetString(tagBytes);

            Debug.Assert(tag == BlockTagName);
            return count;
        }

        private int SaveBlockHeader()
        {
            return SaveDataToWorkBuffer(BlockTagName.GetAsciiBytes(), 5);
        }

        private Dummy LoadDummy(int size)
        {
            LoadDataFromWorkBuffer(size, out byte[] data);
            Dummy obj = new Dummy(size);
            int count = Serializer.Read(obj, data, FileFormat);

            Debug.Assert(count == size);
            return obj;
        }

        private T LoadObject<T>(int size) where T : SaveDataObject, new()
        {
            LoadDataFromWorkBuffer(size, out byte[] data);
            int count = Serializer.Read(data, FileFormat, out T obj);

            Debug.Assert(size == count);
            return obj;
        }

        private int SaveObject(SaveDataObject o)
        {
            int count = Serializer.Write(o, FileFormat, out byte[] data);
            int written = SaveDataToWorkBuffer(data, count);

            Debug.Assert(written == count);
            return written;
        }

        private int LoadDataFromWorkBuffer(int count, out byte[] data)
        {
            if (count <= 0)
            {
                data = new byte[0];
                return 0;
            }

            int total = 0;
            int index = 0;

            data = new byte[count];
            if (m_workBuffer.Cursor + count > m_bufferSize)
            {
                int len = m_bufferSize - m_workBuffer.Cursor;
                total += LoadDataFromWorkBuffer(len, out byte[] tmp);
                Array.Copy(tmp, 0, data, 0, len);
                LoadWorkBuffer();
                count -= len;
                index += len;
            }


            total += m_workBuffer.Read(data, index, count);
            return total;
        }

        private int SaveDataToWorkBuffer(byte[] data, int count)
        {
            if (count <= 0)
            {
                return 0;
            }

            int total = 0;
            int index = 0;

            if (m_workBuffer.Cursor + count > m_bufferSize)
            {
                int len = m_bufferSize - m_workBuffer.Cursor;
                total += SaveDataToWorkBuffer(data, len);
                SaveWorkBuffer(false);
                count -= len;
                index += len;
            }

            total += m_workBuffer.Write(data, index, count);
            return total;
        }

        private void LoadWorkBuffer()
        {
            int count;

            count = m_bufferSize;
            if (m_file.Cursor + count > m_file.Length)
            {
                count = m_file.Length - m_file.Cursor;
            }

            if (count != 0 && count == StreamBuffer.Align4Bytes(count))
            {
                m_workBuffer.Reset();
                m_workBuffer.Write(m_file.ReadBytes(count));
                m_workBuffer.Reset();
            }
        }

        private void SaveWorkBuffer(bool writeCheckSum)
        {
            m_checkSum += m_workBuffer.GetBytes().Sum(x => x);
            if (writeCheckSum)
            {
                if (m_workBuffer.Cursor > m_bufferSize - 4)
                {
                    SaveWorkBuffer(false);
                }
                m_workBuffer.Write(m_checkSum);
            }

            m_file.Write(m_workBuffer.GetBytes());
            m_workBuffer.Reset();
        }

        protected override void LoadAllData(StreamBuffer file)
        {
            List<int> blockSizes;
            byte[] fileData;
            int numBlocks, numCounted;
            int size, offset;
            int mark;

            m_file = file;
            fileData = file.GetAllBytes();
            blockSizes = new List<int>();
            
            offset = fileData.FindFirst(BlockTagName.GetAsciiBytes(), 0);
            numCounted = 0;

            // Get block sizes
            while (offset > -1)
            {
                offset += BlockHeaderSize;
                mark = offset;
                offset = fileData.FindFirst(BlockTagName.GetAsciiBytes(), mark);

                // Padding after final block has no clear beginning marker,
                // need to know exact size of last block
                if (numCounted == 27 && !FileFormat.IsSupportedOnMobile)
                {
                    size = 0x8C;    // TODO: use SizeOf<C3dMarkers>()
                }
                else if (numCounted == 28 && FileFormat.IsSupportedOnMobile)
                {
                    size = 0x160;   // TODO: use SizeOf<PostEffects>();
                }
                else
                {
                    size = offset - mark;
                }

                blockSizes.Add(size);
                numCounted++;
            }

            numBlocks = (FileFormat.IsSupportedOnMobile) ? BlockCountMobile : BlockCount;
            Debug.Assert(numCounted >= numBlocks);

            // Init pointer at end so loader thinks buffer is full and refills it
            m_workBuffer.Seek(m_bufferSize);
            
            for (offset = 0; offset < numBlocks; offset++)
            {
                size = blockSizes[offset];
                LoadBlockHeader();

                switch (offset)
                {
                    case 0: SimpleVars = LoadObject<SimpleVariables>(size); break;
                    case 1: Scripts = LoadDummy(size); break;
                    case 2: Pools = LoadDummy(size); break;
                    case 3: Garages = LoadDummy(size); break;
                    case 4: GameLogic = LoadDummy(size); break;
                    case 5: Paths = LoadDummy(size); break;
                    case 6: Pickups = LoadDummy(size); break;
                    case 7: PhoneInfo = LoadDummy(size); break;
                    case 8: RestartPoints = LoadDummy(size); break;
                    case 9: RadarBlips = LoadDummy(size); break;
                    case 10: Zones = LoadDummy(size); break;
                    case 11: GangData = LoadDummy(size); break;
                    case 12: CarGenerators = LoadDummy(size); break;
                    case 13: PedGenerators = LoadDummy(size); break;
                    case 14: AudioScriptObjects = LoadDummy(size); break;
                    case 15: PlayerInfo = LoadDummy(size); break;
                    case 16: Stats = LoadDummy(size); break;
                    case 17: SetPieces = LoadDummy(size); break;
                    case 18: Streaming = LoadDummy(size); break;
                    case 19: PedTypeInfo = LoadDummy(size); break;
                    case 20: Tags = LoadDummy(size); break;
                    case 21: IplStore = LoadDummy(size); break;
                    case 22: Shopping = LoadDummy(size); break;
                    case 23: GangWars = LoadDummy(size); break;
                    case 24: StuntJumps = LoadDummy(size); break;
                    case 25: EntryExits = LoadDummy(size); break;
                    case 26: Radio = LoadDummy(size); break;
                    case 27: User3dMarkers = LoadDummy(size); break;
                    case 28: PostEffects = LoadDummy(size); break;      // mobile only
                }
                Debug.WriteLine("Read {0} bytes of block data.", size);
            }

            // (mobile) TODO: briefs??
        }

        protected override void SaveAllData(StreamBuffer file)
        {
            int index;
            int size;
            int count;

            m_file = file;
            m_checkSum = 0;
            m_workBuffer.Seek(0);

            count = (FileFormat.IsSupportedOnMobile) ? BlockCountMobile : BlockCount;
            for (index = 0; index < count; index++)
            {
                size = 0;
                SaveBlockHeader();

                switch (index)
                {
                    case 0: size = SaveObject(SimpleVars); break;
                    case 1: size = SaveObject(Scripts); break;
                    case 2: size = SaveObject(Pools); break;
                    case 3: size = SaveObject(Garages); break;
                    case 4: size = SaveObject(GameLogic); break;
                    case 5: size = SaveObject(Paths); break;
                    case 6: size = SaveObject(Pickups); break;
                    case 7: size = SaveObject(PhoneInfo); break;
                    case 8: size = SaveObject(RestartPoints); break;
                    case 9: size = SaveObject(RadarBlips); break;
                    case 10: size = SaveObject(Zones); break;
                    case 11: size = SaveObject(GangData); break;
                    case 12: size = SaveObject(CarGenerators); break;
                    case 13: size = SaveObject(PedGenerators); break;
                    case 14: size = SaveObject(AudioScriptObjects); break;
                    case 15: size = SaveObject(PlayerInfo); break;
                    case 16: size = SaveObject(Stats); break;
                    case 17: size = SaveObject(SetPieces); break;
                    case 18: size = SaveObject(Streaming); break;
                    case 19: size = SaveObject(PedTypeInfo); break;
                    case 20: size = SaveObject(Tags); break;
                    case 21: size = SaveObject(IplStore); break;
                    case 22: size = SaveObject(Shopping); break;
                    case 23: size = SaveObject(GangWars); break;
                    case 24: size = SaveObject(StuntJumps); break;
                    case 25: size = SaveObject(EntryExits); break;
                    case 26: size = SaveObject(Radio); break;
                    case 27: size = SaveObject(User3dMarkers); break;
                    case 28: size = SaveObject(PostEffects); break;     // mobile only
                }
                Debug.WriteLine("Wrote {0} bytes of block data.", size);
            }

            // (mobile) TODO: briefs??

            // Android
            //{
            //    // Padding
            //    if (m_workBuffer.Position > BufferSize - 4)
            //    {
            //        m_workBuffer.Seek(BufferSize);
            //        SaveWorkBuffer(false);
            //    }
            //    else
            //    {
            //        m_workBuffer.Seek(BufferSize - 4);
            //    }
            //    // Checksum
            //    SaveWorkBuffer(true);
            //}

            // PC
            size = m_workBuffer.Cursor + file.Cursor;
            while (size < SizeOfOneGameInBytes - 4)
            {
                int remaining = SizeOfOneGameInBytes - file.Cursor - m_workBuffer.Cursor - 4;
                if (m_workBuffer.Cursor + remaining < m_bufferSize)
                {
                    m_workBuffer.Skip(remaining);
                    break;
                }

                m_workBuffer.Seek(m_bufferSize);
                SaveWorkBuffer(false);
                size = m_workBuffer.Cursor + file.Cursor;
            }
            SaveWorkBuffer(true);

            Debug.Assert(m_file.Cursor == SizeOfOneGameInBytes);
        }

        protected override bool DetectFileFormat(byte[] data, out DataFormat fmt)
        {
            // TODO
            fmt = FileFormats.PC;
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SanAndreasSave);
        }

        public bool Equals(SanAndreasSave other)
        {
            if (other == null)
            {
                return false;
            }

            return SimpleVars.Equals(other.SimpleVars)
                && Scripts.Equals(other.Scripts)
                && Pools.Equals(other.Pools)
                && Garages.Equals(other.Garages)
                && GameLogic.Equals(other.GameLogic)
                && Paths.Equals(other.Paths)
                && Pickups.Equals(other.Pickups)
                && PhoneInfo.Equals(other.PhoneInfo)
                && RestartPoints.Equals(other.RestartPoints)
                && RadarBlips.Equals(other.RadarBlips)
                && Zones.Equals(other.Zones)
                && GangData.Equals(other.GangData)
                && CarGenerators.Equals(other.CarGenerators)
                && PedGenerators.Equals(other.PedGenerators)
                && AudioScriptObjects.Equals(other.AudioScriptObjects)
                && PlayerInfo.Equals(other.PlayerInfo)
                && Stats.Equals(other.Stats)
                && SetPieces.Equals(other.SetPieces)
                && Streaming.Equals(other.Streaming)
                && PedTypeInfo.Equals(other.PedTypeInfo)
                && Tags.Equals(other.Tags)
                && IplStore.Equals(other.IplStore)
                && Shopping.Equals(other.Shopping)
                && GangWars.Equals(other.GangWars)
                && StuntJumps.Equals(other.StuntJumps)
                && EntryExits.Equals(other.EntryExits)
                && Radio.Equals(other.Radio)
                && User3dMarkers.Equals(other.User3dMarkers)
                && PostEffects.Equals(other.PostEffects);
        }

        public void Dispose()
        {
            if (!m_disposed)
            {
                m_workBuffer.Dispose();
                m_disposed = true;
            }
        }

        public static class FileFormats
        {
            // TODO: 1.05 and 1.06 different?
            public static readonly DataFormat Mobile = new DataFormat(
                "Mobile", "Mobile", "Android, iOS",
                new GameConsole(ConsoleType.Android),
                new GameConsole(ConsoleType.iOS)
            );

            public static readonly DataFormat PC = new DataFormat(
                "PC", "PC", "Windows, macOS",
                new GameConsole(ConsoleType.Win32),
                new GameConsole(ConsoleType.MacOS),
                new GameConsole(ConsoleType.Win32, ConsoleFlags.Steam),
                new GameConsole(ConsoleType.MacOS, ConsoleFlags.Steam)
            );

            public static readonly DataFormat PS2 = new DataFormat(
                "PS2", "PS2", "PlayStation 2",
                new GameConsole(ConsoleType.PS2)
            );

            public static readonly DataFormat Xbox = new DataFormat(
                "Xbox", "Xbox", "Xbox",
                new GameConsole(ConsoleType.Xbox)
            );

            public static DataFormat[] GetAll()
            {
                return new DataFormat[] { Mobile, PC, PS2, Xbox };
            }
        }
    }
}
