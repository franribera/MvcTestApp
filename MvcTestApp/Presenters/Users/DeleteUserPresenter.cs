using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Presenters.Users
{
    public class DeleteUserPresenter : IDeleteUserPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void Handle(Response<User> response)
        {
            ActionResult = response.SuccessFul ? (IActionResult)new NoContentResult() : (IActionResult)new NotFoundResult();
        }
    }
}