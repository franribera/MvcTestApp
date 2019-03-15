using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;
using MvcTestApp.Models.Users;

namespace MvcTestApp.Presenters.Users
{
    public class CreateUserPresenter : ICreateUserPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void Handle(Response<User> response)
        {
            ActionResult = response.SuccessFul
                ? (IActionResult)new CreatedResult($@"users\{response.Entity.Id}", MapToUserModel(response.Entity))
                : (IActionResult)new ConflictObjectResult(response.Errors);
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
