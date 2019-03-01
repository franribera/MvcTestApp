using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Infrastructure;

namespace MvcTestApp.Domain.Tests.Infrastructure
{
    [TestClass]
    public class EntityTests
    {
        public class DummyEntity : Entity
        {
            public void ChangeId(Guid id)
            {
                Id = id;
            }
        }

        public class DummyChildEntity : DummyEntity
        { }

        [TestMethod]
        public void Equality_NullEqualsNull_ReturnsTrue()
        {
            // Arrange
            DummyEntity entity1 = null;
            DummyEntity entity2 = null;

            // Act - Assert
            Assert.IsTrue(entity1 == entity2);
        }

        [TestMethod]
        public void Equality_NotNullEqualsNull_ReturnsFalse()
        {
            // Arrange
            DummyEntity entity1 = new DummyEntity();
            DummyEntity entity2 = null;

            // Act - Assert
            Assert.IsFalse(entity1 == entity2);
            Assert.IsFalse(entity1.Equals(entity2));
        }

        [TestMethod]
        public void Equality_EntityEqualsDifferentType_ReturnsFalse()
        {
            // Arrange
            DummyEntity entity1 = new DummyEntity();
            DummyEntity entity2 = new DummyChildEntity();

            // Act - Assert
            Assert.IsFalse(entity1 == entity2);
            Assert.IsFalse(entity1.Equals(entity2));
            Assert.IsFalse(entity2.Equals(entity1));
        }

        [TestMethod]
        public void Equality_SameReference_ReturnsTrue()
        {
            // Arrange
            DummyEntity entity1 = new DummyEntity();
            DummyEntity entity2 = entity1;

            // Act - Assert
            Assert.IsTrue(entity1 == entity2);
            Assert.IsTrue(entity1.Equals(entity2));
            Assert.IsTrue(entity2.Equals(entity1));
        }

        [TestMethod]
        public void Equality_NotNullAndSameTypeButGuidEmptyId_ReturnsFalse()
        {
            // Arrange
            DummyEntity entity1 = new DummyEntity();
            entity1.ChangeId(Guid.Empty);
            DummyEntity entity2 = new DummyEntity();

            // Act - Assert
            Assert.IsFalse(entity1 == entity2);
            Assert.IsFalse(entity1.Equals(entity2));
            Assert.IsFalse(entity2.Equals(entity1));
        }

        [TestMethod]
        public void Equality_NotNullAndSameTypeAndSameId_ReturnsTrue()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            DummyEntity entity1 = new DummyEntity();
            entity1.ChangeId(entityId);
            DummyEntity entity2 = new DummyEntity();
            entity2.ChangeId(entityId);

            // Act - Assert
            Assert.IsTrue(entity1 == entity2);
            Assert.IsTrue(entity1.Equals(entity2));
            Assert.IsTrue(entity2.Equals(entity1));
        }
    }
}
