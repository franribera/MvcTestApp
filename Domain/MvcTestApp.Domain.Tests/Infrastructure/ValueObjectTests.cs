using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Infrastructure;

namespace MvcTestApp.Domain.Tests.Infrastructure
{
    [TestClass]
    public class ValueObjectTests
    {
        private class DummyValueObject : ValueObject
        {
            public string Value1 { get; }
            public string Value2 { get; }

            public DummyValueObject(string value1, string value2)
            {
                Value1 = value1;
                Value2 = value2;
            }

            /// <inheritdoc />
            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return Value1;
                yield return Value2;
            }
        }

        [TestMethod]
        public void Equality_NullEqualsNull_ReturnsTrue()
        {
            // Arrange
            DummyValueObject vo1 = null;
            DummyValueObject vo2 = null;

            // Act - Assert
            Assert.IsTrue(vo1 == vo2);
        }

        [TestMethod]
        public void Equality_InstanceEqualsNull_ReturnsFalse()
        {
            // Arrange
            DummyValueObject vo1 = new DummyValueObject("value1", "value2");
            DummyValueObject vo2 = null;

            // Act - Assert
            Assert.IsFalse(vo1 == vo2);
            Assert.IsFalse(vo1.Equals(vo2));
        }

        [TestMethod]
        public void Equality_DifferentInstancesWithSameValues_ReturnsTrue()
        {
            // Arrange
            var vo1 = new DummyValueObject("value1", "value2");
            var vo2 = new DummyValueObject("value1", "value2"); ;

            // Act - Assert
            Assert.IsTrue(vo1 == vo2);
            Assert.IsTrue(vo1.Equals(vo2));
        }

        [TestMethod]
        public void Equality_DifferentInstancesWithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var vo1 = new DummyValueObject("value1", "value2");
            var vo2 = new DummyValueObject("value1", null);

            // Act - Assert
            Assert.IsFalse(vo1 == vo2);
            Assert.IsFalse(vo1.Equals(vo2));
        }
    }
}