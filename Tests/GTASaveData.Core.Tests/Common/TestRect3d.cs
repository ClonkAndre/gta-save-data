﻿using Bogus;
using GTASaveData.Common;
using GTASaveData.Serialization;
using TestFramework;
using Xunit;

namespace GTASaveData.Core.Tests.Common
{
    public class TestRect3d : SerializableObjectTestBase<Rect3d>
    {
        public override Rect3d GenerateTestVector(FileFormat format)
        {
            Faker<Rect3d> model = new Faker<Rect3d>()
                .RuleFor(x => x.XMin, f => f.Random.Float(-4000, 4000))
                .RuleFor(x => x.XMax, f => f.Random.Float(-4000, 4000))
                .RuleFor(x => x.YMin, f => f.Random.Float(-4000, 4000))
                .RuleFor(x => x.YMax, f => f.Random.Float(-4000, 4000))
                .RuleFor(x => x.ZMin, f => f.Random.Float(-4000, 4000))
                .RuleFor(x => x.ZMax, f => f.Random.Float(-4000, 4000));

            return model.Generate();
        }

        [Fact]
        public void Serialization()
        {
            Rect3d x0 = GenerateTestVector();
            Rect3d x1 = CreateSerializedCopy(x0, out byte[] data);

            Assert.Equal(x0.XMin, x1.XMin);
            Assert.Equal(x0.XMax, x1.XMax);
            Assert.Equal(x0.YMin, x1.YMin);
            Assert.Equal(x0.YMax, x1.YMax);
            Assert.Equal(x0.ZMin, x1.ZMin);
            Assert.Equal(x0.ZMax, x1.ZMax);
            Assert.Equal(x0, x1);
            Assert.Equal(24, data.Length);
        }
    }
}
