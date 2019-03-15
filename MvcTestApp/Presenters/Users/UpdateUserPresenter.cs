using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Commands.Users.Update;
using MvcTestApp.Domain.Users;
using MvcTestApp.Models.Users;

namespace MvcTestApp.Presenters.Users
{
    public class UpdateUserPresenter : IUpdateUserPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void Handle(UpdateUserResponse response)
        {
            if(response.SuccessFul) ActionResult = new OkObjectResult(MapToUserModel(response.User));
            else ActionResult = response.NotFound ? (IActionResult) new NotFoundResult() : new ConflictResult();
        }

        private static UserModel MapToUserModel(User user)
        {
            return new UserModel
            {
                UserName = user.UserName.Value,
                Id = user.Id,
                Roles = user.Roles.Select(role => role.Name.Value).ToArray()
            };
        }
    }
}