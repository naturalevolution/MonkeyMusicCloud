using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Domain.Model
{
    [TestClass]
    public class EntityShould
    {
        [TestMethod]
        public void CompareWithEquals()
        {   Guid id = Guid.NewGuid();
            Entity entity1 = new Entity(){Id = id};
            Entity entity2 = new Entity(){Id = id};
            Assert.IsTrue(entity1.Equals(entity2));

            Object obj1 = new object();
            Assert.IsFalse(entity1.Equals(obj1));

            Object obj2 = null;
            Assert.IsFalse(entity1.Equals(obj2));

        }

        [TestMethod]
        public void CompareWithEqualOperator()
        {
            Guid id = Guid.NewGuid();
            Entity entity1 = new Entity() { Id = id };
            Entity entity2 = new Entity() { Id = id };
            Assert.IsTrue(entity1 == entity2);
        }

        [TestMethod]
        public void CompareWithInequalOperator()
        {
            Guid id = Guid.NewGuid();
            Entity entity1 = new Entity() { Id = id };
            Entity entity2 = new Entity() { Id = id };
            Assert.IsFalse(entity1 != entity2);
        }
    }
}