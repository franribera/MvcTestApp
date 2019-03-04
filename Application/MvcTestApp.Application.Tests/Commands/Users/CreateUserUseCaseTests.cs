using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Application.Commands.Users.Create;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Tests.Commands.Users
{
    [TestClass]
    public class CreateUserUseCaseTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IOutputPort<Response<User>>> _outputPortMock;

        private CreateUserUseCase _createUserUseCase;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _outputPortMock = new Mock<IOutputPort<Response<User>>>();

            _createUserUseCase = new CreateUserUseCase(_userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_ExistingUser_PassesFailedResultToOutputPort()
        {
            // Arrange
            var createUserRequest = new CreateUserRequest("userName", "password", new[] {"PAGE_1"});
            var existingUser = new UserBuilder().Build();
            _userRepositoryMock.Setup(mock => mock.Get(It.IsAny<Name>())).ReturnsAsync(existingUser);

            // Act
            await _createUserUseCase.Handle(createUserRequest, _outputPortMock.Object);

            // Assert
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul == false)));
        }

        [TestMethod]
        public async Task Handle_NotExistingUser_CreatesUserAndPassesSuccessfullResultToOutputPort()
        {
            // Arrange
            const string userName = "userName";
            const string password = "password;";
            var createUserRequest = new CreateUserRequest(userName, password,
                new[] {Role.PAGE_1.Name.Value, Role.PAGE_2.Name.Value});
            var expectedUserName = new Name(userName);
            var expectedPassword = new Password(password);
            var expectedRoles = new Role[] {Role.PAGE_1, Role.PAGE_2};
            _userRepositoryMock.Setup(mock => mock.Get(It.IsAny<Name>())).ReturnsAsync((User)null);

            // Act
            await _createUserUseCase.Handle(createUserRequest, _outputPortMock.Object);

            // Assert
            _userRepositoryMock.Verify(mock => mock.Add(It.Is<User>(user => 
                user.UserName == expectedUserName && 
                user.Password == expectedPassword &&
                user.Roles.Select(role => role.Name).All(role => expectedRoles.Select(expectedRole => expectedRole.Name).Contains(role)))));
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul)));
        }
    }
}
