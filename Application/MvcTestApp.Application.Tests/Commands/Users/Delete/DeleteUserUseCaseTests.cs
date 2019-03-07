using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Application.Commands.Users.Delete;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;

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
            var userId = Guid.NewGuid();
            var deleteUserRequest = new DeleteUserRequest(userId);
            _userRepositoryMock
                .Setup(mock => mock.Get(userId))
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
            var deleteUserRequest = new DeleteUserRequest(existingUser.Id);
            _userRepositoryMock
                .Setup(mock => mock.Get(existingUser.Id))
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