﻿using Bogus;
using GTASaveData.GTA3;
using GTASaveData.GTA3.Blocks;
using GTASaveData.Serialization;
using GTASaveData.Tests.GTA3.Blocks;
using System.Collections.Generic;
using TestFramework;
using Xunit;

namespace GTASaveData.Tests.GTA3
{
    public class TestGTA3Save : SerializableObjectTestBase<GTA3Save>
    {
        public override GTA3Save GenerateTestVector(FileFormat format)
        {
            Faker faker = new Faker();

            Faker<GTA3Save> model = new Faker<GTA3Save>()
                .RuleFor(x => x.SimpleVars, f => Generator.Generate<SimpleVars, TestSimpleVars>(format))
                .RuleFor(x => x.Scripts, Generator.Generate<ScriptBlock, TestScriptBlock>(format))
                //.RuleFor(x => x.PedPool, TestHelper.Generate<PedPool, TestPedPool>(format))
                .RuleFor(x => x.Garages, Generator.Generate<GarageBlock, TestGarageBlock>(format))
                .RuleFor(x => x.Vehicles, Generator.Generate<VehiclePool, TestVehiclePool>(format))
                //.RuleFor(x => x.Objects, Generator.Generate<Objects, TestObjects>(format))
                //.RuleFor(x => x.PathFind, Generator.Generate<PathFind, TestPathFind>(format))
                //.RuleFor(x => x.Cranes, Generator.Generate<Cranes, TestCranes>(format))
                .RuleFor(x => x.Pickups, Generator.Generate<PickupBlock, TestPickupBlock>(format))
                //.RuleFor(x => x.PhoneInfo, Generator.Generate<PhoneInfo, TestPhoneInfo>(format))
                //.RuleFor(x => x.RestartPoints, Generator.Generate<RestartPoints, TestRestartPoints>(format))
                //.RuleFor(x => x.RadarBlips, Generator.Generate<RadarBlips, TestRadarBlips>(format))
                //.RuleFor(x => x.Zones, Generator.Generate<Zones, TestZones>(format))
                //.RuleFor(x => x.GangData, Generator.Generate<GangData, TestGangData>(format))
                .RuleFor(x => x.CarGenerators, Generator.Generate<CarGeneratorBlock, TestCarGeneratorBlock>(format))
                //.RuleFor(x => x.Particles, Generator.Generate<Particles, TestParticles>(format))
                //.RuleFor(x => x.AudioScriptObjects, Generator.Generate<AudioScriptObjects, TestAudioScriptObjects>(format))
                //.RuleFor(x => x.PlayerInfo, Generator.Generate<PlayerInfo, TestPlayerInfo>(format))
                //.RuleFor(x => x.Stats, Generator.Generate<Stats, TestStats>(format))
                //.RuleFor(x => x.Streaming, Generator.Generate<Streaming, TestStreaming>(format))
                //.RuleFor(x => x.PedTypeInfo, Generator.Generate<PedTypeInfo, TestPedTypeInfo>(format))
                ;

            return model.Generate();
        }

        [Theory]
        [MemberData(nameof(SerializationData))]
        public void Serialization(FileFormat format)
        {
            GTA3Save x0 = GenerateTestVector(format);
            GTA3Save x1 = CreateSerializedCopy(x0, format);

            Assert.Equal(x0.SimpleVars, x1.SimpleVars);
            Assert.Equal(x0.Scripts, x1.Scripts);
            //Assert.Equal(x0.PedPool, x1.PedPool);
            Assert.Equal(x0.Garages, x1.Garages);
            Assert.Equal(x0.Vehicles, x1.Vehicles);
            Assert.Equal(x0.Pickups, x1.Pickups);
            Assert.Equal(x0.CarGenerators, x1.CarGenerators);

            Assert.Equal(x0, x1);
            // TODO: data size check?
        }

        [Theory]
        [MemberData(nameof(FileFormatData))]
        public void FileTypeDetection(FileFormat expectedFileFormat, string filePath)
        {
            FileFormat detected = GrandTheftAutoSave.GetFileFormat<GTA3Save>(filePath);

            Assert.Equal(expectedFileFormat, detected);
        }

        public static IEnumerable<object[]> FileFormatData => new[]
        {
            // TODO: testdata access in testFramework
            new object[] { GTA3Save.FileFormats.PC, "./TestData/GTA3/PC/1_JM4" },
            new object[] { GTA3Save.FileFormats.PC, "./TestData/GTA3/PC/2_AS3" },
            new object[] { GTA3Save.FileFormats.PS2AU, "./TestData/GTA3/PS2AU/1_T4X4_2" },
            new object[] { GTA3Save.FileFormats.PS2AU, "./TestData/GTA3/PS2AU/2_AS3" },
            new object[] { GTA3Save.FileFormats.PS2AU, "./TestData/GTA3/PS2AU/3_CAT2" },
            new object[] { GTA3Save.FileFormats.PS2JP, "./TestData/GTA3/PS2JP/1_LM1" },
            new object[] { GTA3Save.FileFormats.PS2JP, "./TestData/GTA3/PS2JP/2_LM2" },
            new object[] { GTA3Save.FileFormats.PS2NAEU, "./TestData/GTA3/PS2NAEU/1_T4X4_1" },
            new object[] { GTA3Save.FileFormats.PS2NAEU, "./TestData/GTA3/PS2NAEU/2_LM1" },
            new object[] { GTA3Save.FileFormats.PS2NAEU, "./TestData/GTA3/PS2NAEU/3_CAT2" },
        };

        public static IEnumerable<object[]> SerializationData => new[]
        {
            new object[] { GTA3Save.FileFormats.Android },
            new object[] { GTA3Save.FileFormats.IOS },
            new object[] { GTA3Save.FileFormats.PC },
            new object[] { GTA3Save.FileFormats.PS2AU },
            new object[] { GTA3Save.FileFormats.PS2JP },
            new object[] { GTA3Save.FileFormats.PS2NAEU },
            new object[] { GTA3Save.FileFormats.Xbox },
        };
    }
}
