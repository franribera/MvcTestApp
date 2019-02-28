using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Tests.ValueObjects
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void Password_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Password(string.Empty));
        }

        [TestMethod]
        public void Password_WhiteSpacesValues_AreTreatedAsEmptyValueAndThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Password(" "));
            Assert.ThrowsException<ArgumentException>(() => new Password("  "));
            Assert.ThrowsException<ArgumentException>(() => new Password("   "));
        }

        [TestMethod]
        public void Password_ComputesMD5HashAndExposesTheValueAsBase64String()
        {
            // Arrange
            const string passwordPlainText = "password";
            const string expectedHashedBase64Result = "sIHb6F4ew//D1OfQInQAzQ==";

            // Act
            var password = new Password(passwordPlainText);

            // Assert
            Assert.AreEqual(expectedHashedBase64Result, password.Value);
        }

        [TestMethod]
        public void Password_EqualsIfValuesAreEqual()
        {
            // Arrange
            const string defaultPassword = "defaultPassword";

            // Act
            var name1 = new Password(defaultPassword);
            var name2 = new Password(defaultPassword);

            // Assert
            Assert.AreEqual(name1, name2);
            Assert.IsFalse(ReferenceEquals(name1, name2));
        }
    }
}
