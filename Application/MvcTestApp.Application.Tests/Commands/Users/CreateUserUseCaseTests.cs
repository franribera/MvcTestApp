using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Application.Commands;
using MvcTestApp.Application.Commands.Users.Create;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Tests.Commands.Users
{
    [TestClass]
    public class CreateUserUseCaseTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserFactory> _userFactoryMock;
        private Mock<IOutputPort<Response<User>>> _outputPortMock;

        private CreateUserUseCase _createUserUseCase;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userFactoryMock = new Mock<IUserFactory>();
            _outputPortMock = new Mock<IOutputPort<Response<User>>>();

            _createUserUseCase = new CreateUserUseCase(_userRepositoryMock.Object, _userFactoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_ExistingUser_PassesFailedResultToOutputPort()
        {
            // Arrange
            var expectedUser = new UserBuilder().WithRoles(Role.PAGE_1).Build();
            var createUserRequest = new CreateUserRequest(expectedUser.UserName.Value, expectedUser.Password.Value,
                expectedUser.Roles.Select(role => role.Name.Value));
            _userFactoryMock.Setup(mock => mock.Create(createUserRequest)).Returns(expectedUser);
            _userRepositoryMock.Setup(mock => mock.Get(expectedUser.UserName)).ReturnsAsync(expectedUser);

            // Act
            await _createUserUseCase.Handle(createUserRequest, _outputPortMock.Object);

            // Assert
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul == false)));
        }

        [TestMethod]
        public async Task Handle_NotExistingUser_CreatesUserAndPassesSuccessfullResultToOutputPort()
        {
            // Arrange
            var expectedUser = new UserBuilder().WithRoles(Role.PAGE_1, Role.PAGE_2).Build();
            var createUserRequest = new CreateUserRequest(expectedUser.UserName.Value, expectedUser.Password.Value,
                expectedUser.Roles.Select(role => role.Name.Value));
            _userFactoryMock.Setup(mock => mock.Create(createUserRequest)).Returns(expectedUser);
            _userRepositoryMock.Setup(mock => mock.Get(expectedUser.UserName)).ReturnsAsync((User)null);

            // Act
            await _createUserUseCase.Handle(createUserRequest, _outputPortMock.Object);

            // Assert
            _userRepositoryMock.Verify(mock => mock.Add(expectedUser));
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul)));
        }
    }
}
