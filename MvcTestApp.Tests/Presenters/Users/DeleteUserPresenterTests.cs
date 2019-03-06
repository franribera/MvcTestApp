using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Tests.Presenters.Users
{
    [TestClass]
    public class DeleteUserPresenterTests
    {
        [TestMethod]
        public void Handle_FailedResult_ReturnsNotFoundActionResult()
        {
            // Arrange
            var response = Response<User>.Fail(new[] { "errorMessage" });
            var presenter = new DeleteUserPresenter();

            // Act
            presenter.Handle(response);

            // Assert
            var actionResult = presenter.ActionResult as NotFoundResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, actionResult.StatusCode);
        }

        [TestMethod]
        public void Handle_SuccessfulResult_ReturnsNoContentActionResult()
        {
            // Arrange
            var response = Response<User>.Success(null);
            var presenter = new DeleteUserPresenter();

            // Act
            presenter.Handle(response);

            // Assert
            var createdActionResult = presenter.ActionResult as NoContentResult;
            Assert.AreEqual((int)HttpStatusCode.NoContent, createdActionResult.StatusCode);
        }
    }
}
