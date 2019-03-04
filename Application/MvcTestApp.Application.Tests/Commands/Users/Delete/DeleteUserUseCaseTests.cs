using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Application.Commands.Users.Delete;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Tests.Commands.Users.Delete
{
    [TestClass]
    public class DeleteUserUseCaseTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IOutputPort<Response<User>>> _outputPortMock;
        private DeleteUserUseCase _deleteUserUseCase;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _outputPortMock = new Mock<IOutputPort<Response<User>>>();
            _deleteUserUseCase = new DeleteUserUseCase(_userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_NotExistingUser_PassesFailedResultToOutputPort()
        {
            // Arrange
            const string userName = "userName";
            var deleteUserRequest = new DeleteUserRequest(userName);
            _userRepositoryMock
                .Setup(mock => mock.Get(It.Is<Name>(name => name.Value == userName)))
                .ReturnsAsync((User) null);

            // Act
            await _deleteUserUseCase.Handle(deleteUserRequest, _outputPortMock.Object);

            // Assert
            _userRepositoryMock.Verify(mock => mock.Delete(It.IsAny<User>()), Times.Never);
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul == false)), Times.Once);
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul)), Times.Never);
        }

        [TestMethod]
        public async Task Handle_ExistingUser_PassesSucssessFulResultToOutputPort()
        {
            // Arrange
            var existingUser = new UserBuilder().Build();
            var deleteUserRequest = new DeleteUserRequest(existingUser.UserName.Value);
            _userRepositoryMock
                .Setup(mock => mock.Get(It.Is<Name>(name => name.Value == existingUser.UserName.Value)))
                .ReturnsAsync(existingUser);

            // Act
            await _deleteUserUseCase.Handle(deleteUserRequest, _outputPortMock.Object);

            // Assert
            _userRepositoryMock.Verify(mock => mock.Delete(It.IsAny<User>()), Times.Once);
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul == false)), Times.Never);
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul)), Times.Once);
        }
    }
}