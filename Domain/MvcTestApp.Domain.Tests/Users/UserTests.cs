using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Domain.Tests.Users
{
    [TestClass]
    public class UserTests
    {
        private User _user;
        private const string DefaultUserName = "defaultUserName";

        [TestInitialize]
        public void TestInitialize()
        {
            _user = new User(DefaultUserName);
        }

        [TestMethod]
        public void SetUserName_SetsValueIntoUserNameField()
        {
            // Arrange

            const string expectedUserName = "userName";

            // Act
            _user.SetUserName(expectedUserName);
            
            // Assert
            Assert.AreEqual(expectedUserName, _user.UserName);
        }

        [TestMethod]
        public void SetUserName_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _user.SetUserName(string.Empty));
        }

        [TestMethod]
        public void SetUserName_WhiteSpacesValues_AreTreatedAsEmptyValueAndThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _user.SetUserName(" "));
            Assert.ThrowsException<ArgumentException>(() => _user.SetUserName("  "));
            Assert.ThrowsException<ArgumentException>(() => _user.SetUserName("   "));
        }

        [TestMethod]
        public void User_RequiresAnUserName()
        {
            // Arrange
            const string expectedUserName = "userName";

            // Act
            var user = new User(expectedUserName);

            // Assert
            Assert.AreEqual(expectedUserName, user.UserName);
        }
    }
}
