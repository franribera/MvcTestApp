using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;
using MvcTestApp.Models.Users;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Tests.Presenters.Users
{
    [TestClass]
    public class CreateUserPresenterTests
    {
        [TestMethod]
        public void Handle_ExistingUserName_ReturnsConflictActionResult()
        {
            // Arrange
            var response = Response<User>.Fail(new[] { "errorMessage" });
            var presenter = new CreateUserPresenter();

            // Act
            presenter.Handle(response);

            // Assert
            var conflictActionResult = presenter.ActionResult as ConflictObjectResult;
            Assert.AreEqual((int)HttpStatusCode.Conflict, conflictActionResult.StatusCode);
            Assert.AreEqual(response.Errors, conflictActionResult.Value);
        }

        [TestMethod]
        public void Handle_UserCreated_ReturnsCreatedActionResultWithTheCreatedUser()
        {
            // Arrange
            var createdUser = new UserBuilder()
                .WithName(new Name("TestName"))
                .WithRoles(new Role(new Name("TestRole")))
                .Build();
            var response = Response<User>.Success(createdUser);
            var presenter = new CreateUserPresenter();

            // Act
            presenter.Handle(response);

            // Assert
            var createdActionResult = presenter.ActionResult as CreatedResult;
            Assert.AreEqual((int)HttpStatusCode.Created, createdActionResult.StatusCode);
            var userModel = createdActionResult.Value as UserModel;
            Assert.AreEqual(createdUser.UserName.Value, userModel.UserName);
            Assert.AreEqual(createdUser.Id, userModel.Id);
            CollectionAssert.AreEquivalent(createdUser.Roles.Select(role => role.Name.Value).ToList(), userModel.Roles.ToList());
        }
    }
}
