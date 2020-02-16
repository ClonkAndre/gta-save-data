﻿using Bogus;
using GTASaveData.GTA3;
using GTASaveData.Serialization;
using TestFramework;
using Xunit;

namespace GTASaveData.Tests.GTA3
{
    public class TestInvisibleObject : SerializableObjectTestBase<InvisibleObject>
    {
        public override InvisibleObject GenerateTestVector(FileFormat format)
        {
            Faker<InvisibleObject> model = new Faker<InvisibleObject>()
                .RuleFor(x => x.Type, f => f.PickRandom<ObjectType>())
                .RuleFor(x => x.StaticIndex, f => f.Random.Int());

            return model.Generate();
        }

        [Fact]
        public void Serialization()
        {
            InvisibleObject x0 = GenerateTestVector();
            InvisibleObject x1 = CreateSerializedCopy(x0, out byte[] data);

            Assert.Equal(x0.Type, x1.Type);
            Assert.Equal(x0.StaticIndex, x1.StaticIndex);
            Assert.Equal(x0, x1);
            Assert.Equal(8, data.Length);
        }
    }
}
