﻿using Bogus;
using TestFramework;
using Xunit;

namespace GTASaveData.GTA3.Tests
{
    public class TestObjectPool : Base<ObjectPool>
    {
        public override ObjectPool GenerateTestObject(DataFormat format)
        {
            Faker<ObjectPool> model = new Faker<ObjectPool>()
                .RuleFor(x => x.Items,
                    f => Generator.Array(f.Random.Int(1, 50), g => Generator.Generate<GameObject, TestGameObject>()));

            return model.Generate();
        }

        [Fact]
        public void RandomDataSerialization()
        {
            ObjectPool x0 = GenerateTestObject();
            ObjectPool x1 = CreateSerializedCopy(x0, out byte[] data);

            Assert.Equal(x0.Items, x1.Items);

            Assert.Equal(x0, x1);
            Assert.Equal(GetSizeOfTestObject(x0), data.Length);
        }
    }
}
