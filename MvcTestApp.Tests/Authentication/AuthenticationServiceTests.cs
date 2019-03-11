using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Authentication;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Tests.Authentication
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private AuthenticationService _authenticationService;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authenticationService = new AuthenticationService(_userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Login_MissingUser_ReturnsNull()
        {
            // Arrange
            const string userName = "userName";
            const string password = "password";
            _userRepositoryMock
                .Setup(mock => mock.Get(It.Is<Name>(name => name.Value == userName)))
                .ReturnsAsync((User) null);

            // Act
            var claimsPrincipal = await _authenticationService.Login(userName, password);

            // Assert
            Assert.IsNull(claimsPrincipal);
        }

        [TestMethod]
        public async Task Login_InvalidPassword_ReturnsNull()
        {
            // Arrange
            const string userName = "userName";
            const string password = "password";
            var user = new UserBuilder()
                .WithName(new Name(userName))
                .Build();

            _userRepositoryMock
                .Setup(mock => mock.Get(It.IsAny<Name>()))
                .ReturnsAsync(user);

            // Act
            var claimsPrincipal = await _authenticationService.Login(userName, password);

            // Assert
            Assert.IsNull(claimsPrincipal);
        }

        [TestMethod]
        public async Task Login_ValidCredentialsForExistingUser_ReturnsClaimsPrincipalWithUserNameAndRoles()
        {
            // Arrange
            const string userName = "userName";
            const string password = "password";
            var user = new UserBuilder()
                .WithName(new Name(userName))
                .WithPassword(new Password(password))
                .WithRoles(Role.ADMIN)
                .Build();

            _userRepositoryMock
                .Setup(mock => mock.Get(It.IsAny<Name>()))
                .ReturnsAsync(user);

            // Act
            var claimsPrincipal = await _authenticationService.Login(userName, password);

            // Assert
            Assert.AreEqual(userName, claimsPrincipal.Identity.Name);
            Assert.IsTrue(claimsPrincipal.IsInRole(Role.ADMIN.Name.Value));
        }
    }
}
