using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Domain.Tests.Users
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void SetUserName_SetsValueIntoUserNameField()
        {
            // Arrange
            var user = new User();
            const string expectedUserName = "userName";

            // Act
            user.SetUserName(expectedUserName);
            
            // Assert
            Assert.AreEqual(expectedUserName, user.UserName);
        }
    }
}
