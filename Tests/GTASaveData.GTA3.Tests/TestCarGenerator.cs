﻿using Bogus;
using Xunit;
using TestFramework;
using GTASaveData.Types;
using GTASaveData.Core.Tests.Types;

namespace GTASaveData.GTA3.Tests
{
    public class TestCarGenerator : Base<CarGenerator>
    {
        public override CarGenerator GenerateTestObject(SaveFileFormat format)
        {
            Faker<CarGenerator> model = new Faker<CarGenerator>()
                .RuleFor(x => x.Model, f => f.Random.Int())
                .RuleFor(x => x.Position, f => Generator.Generate<Vector, TestVector>())
                .RuleFor(x => x.Angle, f => f.Random.Float())
                .RuleFor(x => x.Color1, f => f.Random.Short())
                .RuleFor(x => x.Color2, f => f.Random.Short())
                .RuleFor(x => x.ForceSpawn, f => f.Random.Bool())
                .RuleFor(x => x.Alarm, f => f.Random.Byte())
                .RuleFor(x => x.DoorLock, f => f.Random.Byte())
                .RuleFor(x => x.MinDelay, f => f.Random.UShort())
                .RuleFor(x => x.MaxDelay, f => f.Random.UShort())
                .RuleFor(x => x.Timer, f => f.Random.UInt())
                .RuleFor(x => x.VehicleHandle, f => f.Random.Int())
                .RuleFor(x => x.UsesRemaining, f => f.Random.Short())
                .RuleFor(x => x.IsBlocking, f => f.Random.Bool())
                .RuleFor(x => x.VecInf, f => Generator.Generate<Vector, TestVector>())
                .RuleFor(x => x.VecSup, f => Generator.Generate<Vector, TestVector>())
                .RuleFor(x => x.Size, f => f.Random.Float());

            return model.Generate();
        }

        [Fact]
        public void Serialization()
        {
            CarGenerator x0 = GenerateTestObject();
            CarGenerator x1 = CreateSerializedCopy(x0, out byte[] data);

            Assert.Equal(x0.Model, x1.Model);
            Assert.Equal(x0.Position, x1.Position);
            Assert.Equal(x0.Angle, x1.Angle);
            Assert.Equal(x0.Color1, x1.Color1);
            Assert.Equal(x0.Color2, x1.Color2);
            Assert.Equal(x0.ForceSpawn, x1.ForceSpawn);
            Assert.Equal(x0.Alarm, x1.Alarm);
            Assert.Equal(x0.DoorLock, x1.DoorLock);
            Assert.Equal(x0.MinDelay, x1.MinDelay);
            Assert.Equal(x0.MaxDelay, x1.MaxDelay);
            Assert.Equal(x0.Timer, x1.Timer);
            Assert.Equal(x0.VehicleHandle, x1.VehicleHandle);
            Assert.Equal(x0.UsesRemaining, x1.UsesRemaining);
            Assert.Equal(x0.IsBlocking, x1.IsBlocking);
            Assert.Equal(x0.VecInf, x1.VecInf);
            Assert.Equal(x0.VecSup, x1.VecSup);
            Assert.Equal(x0.Size, x1.Size);
            Assert.Equal(x0, x1);
            Assert.Equal(GetSizeOfTestObject(), data.Length);
        }
    }
}
