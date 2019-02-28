using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Tests.ValueObjects
{
    [TestClass]
    public class NameTests
    {
        [TestMethod]
        public void Name_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Name(string.Empty));
        }

        [TestMethod]
        public void Name_WhiteSpacesValues_AreTreatedAsEmptyValueAndThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Name(" "));
            Assert.ThrowsException<ArgumentException>(() => new Name("  "));
            Assert.ThrowsException<ArgumentException>(() => new Name("   "));
        }

        [TestMethod]
        public void Name_EqualsIfValuesAreEqual()
        {
            // Arrange
            const string defaultName = "defaultName";

            // Act
            var name1 = new Name(defaultName);
            var name2 = new Name(defaultName);

            // Assert
            Assert.AreEqual(name1, name2);
            Assert.IsFalse(ReferenceEquals(name1, name2));
        }
    }
}