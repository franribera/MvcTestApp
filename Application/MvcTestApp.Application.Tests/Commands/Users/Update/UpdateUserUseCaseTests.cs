using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Application.Commands.Users.Update;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Tests.Commands.Users.Update
{
    [TestClass]
    public class UpdateUserUseCaseTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IOutputPort<Response<User>>> _outputPortMock;

        private UpdateUserUseCase _updateUserUseCase;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _outputPortMock = new Mock<IOutputPort<Response<User>>>();
            _updateUserUseCase = new UpdateUserUseCase(_userRepositoryMock.Object);

        }

        [TestMethod]
        public async Task Handle_NotExistingUser_PassesFailResultToOutputPort()
        {
            // Arrange
            var request = new UpdateUserRequest(Guid.NewGuid(), "", "", new List<string>());
            _userRepositoryMock.Setup(mock => mock.Get(request.Id)).ReturnsAsync((User) null);

            // Act
            await _updateUserUseCase.Handle(request, _outputPortMock.Object);

            // Assert
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul == false)));
        }

        [TestMethod]
        public async Task Handle_DuplicatedUserName_PassesFailResultToOutputPort()
        {
            // Arrange
            var request = new UpdateUserRequest(Guid.NewGuid(), "duplicatedName", "", new List<string>());
            var existingUser = new UserBuilder().Build();
            _userRepositoryMock.Setup(mock => mock.Get(request.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(mock => mock.Get(It.Is<Name>(name => name.Value == request.UserName))).ReturnsAsync(existingUser);

            // Act
            await _updateUserUseCase.Handle(request, _outputPortMock.Object);

            // Assert
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul == false)));
        }

        [TestMethod]
        public async Task Handle_DoesNotUpdateNullName_PassesSuccessfulResultToOutputPort()
        {
            // Arrange
            var request = new UpdateUserRequest(Guid.NewGuid(), null, "password", new List<string> { Role.PAGE_2.Name.Value });
            var existingUser = new UserBuilder().WithRoles(Role.PAGE_1).Build();

            var expectedUserName = existingUser.UserName.Value;
            const string expectedPassword = "sIHb6F4ew//D1OfQInQAzQ==";
            var expectedRole = Role.PAGE_2;

            // Act - Assert
            await TestUpdate(request, existingUser, expectedUserName, expectedPassword, expectedRole);
        }

        [TestMethod]
        public async Task Handle_DoesNotUpdateNullPassword_PassesSuccessfulResultToOutputPort()
        {
            // Arrange
            var request = new UpdateUserRequest(Guid.NewGuid(), "username", null, new List<string> { Role.PAGE_2.Name.Value });
            var existingUser = new UserBuilder().WithRoles(Role.PAGE_1).Build();

            var expectedUserName = request.UserName;
            var expectedPassword = existingUser.Password.Value;
            var expectedRole = Role.PAGE_2;

            // Act - Assert
            await TestUpdate(request, existingUser, expectedUserName, expectedPassword, expectedRole);
        }

        [TestMethod]
        public async Task Handle_DoesNotUpdateNullRolesCollection_PassesSuccessfulResultToOutputPort()
        {
            // Arrange
            var request = new UpdateUserRequest(Guid.NewGuid(), "username", "password", null);
            var existingUser = new UserBuilder().WithRoles(Role.PAGE_1).Build();

            var expectedUserName = request.UserName;
            const string expectedPassword = "sIHb6F4ew//D1OfQInQAzQ==";
            var expectedRole = Role.PAGE_1;

            // Act - Assert
            await TestUpdate(request, existingUser, expectedUserName, expectedPassword, expectedRole);
        }

        private async Task TestUpdate(UpdateUserRequest request, User existingUser, string expectedUserName, string expectedPassword, Role expectedRole)
        {
            // Arrange
            _userRepositoryMock.Setup(mock => mock.Get(request.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(mock => mock.Get(It.Is<Name>(name => name.Value == request.UserName))).ReturnsAsync((User)null);

            // Act
            await _updateUserUseCase.Handle(request, _outputPortMock.Object);

            // Assert
            Assert.AreEqual(expectedUserName, existingUser.UserName.Value);
            Assert.AreEqual(expectedPassword, existingUser.Password.Value);
            Assert.AreEqual(expectedRole, existingUser.Roles.Single());

            _userRepositoryMock.Verify(mock => mock.Update(existingUser));
            _outputPortMock.Verify(mock => mock.Handle(It.Is<Response<User>>(response => response.SuccessFul)));
        }
    }
}