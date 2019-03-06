using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Application.Commands.Users.Update;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;
using MvcTestApp.Models.Users;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Tests.Presenters.Users
{
    [TestClass]
    public class UpdateUserPresenterTests
    {
        [TestMethod]
        public void Handle_UserNotFound_ReturnsNotFoundActionResult()
        {
            // Arrange
            var response = UpdateUserResponse.UserNotFound(new []{"Whatever"});
            var presenter = new UpdateUserPresenter();

            // Act
            presenter.Handle(response);

            // Assert
            var actionResult = presenter.ActionResult as NotFoundResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, actionResult.StatusCode);
        }

        [TestMethod]
        public void Handle_DuplicatedUserName_ReturnsConflictActionResult()
        {
            // Arrange
            var response = UpdateUserResponse.DuplicatedUserName(new[] { "Whatever" });
            var presenter = new UpdateUserPresenter();

            // Act
            presenter.Handle(response);

            // Assert
            var actionResult = presenter.ActionResult as ConflictResult;
            Assert.AreEqual((int)HttpStatusCode.Conflict, actionResult.StatusCode);
        }

        [TestMethod]
        public void Handle_UserUpdated_ReturnsOkActionResultWithTheUpdatedUser()
        {
            // Arrange
            var createdUser = new UserBuilder()
                .WithName(new Name("TestName"))
                .WithRoles(new Role(new Name("TestRole")))
                .Build();
            var response = UpdateUserResponse.Success(createdUser);
            var presenter = new UpdateUserPresenter();

            // Act
            presenter.Handle(response);

            // Assert
            var createdActionResult = presenter.ActionResult as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, createdActionResult.StatusCode);
            var userModel = createdActionResult.Value as UserModel;
            Assert.AreEqual(createdUser.UserName.Value, userModel.UserName);
            Assert.AreEqual(createdUser.Id, userModel.Id);
            CollectionAssert.AreEquivalent(createdUser.Roles.Select(role => role.Name.Value).ToList(), userModel.Roles.ToList());
        }
    }
}