using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Commands.Users.Create;
using MvcTestApp.Authentication;
using MvcTestApp.Models.Users;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Controllers.Users
{
    [Route("api/users")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly ICreateUserPresenter _createUserPresenter;

        public CreateUserController(ICreateUserUseCase createUserUseCase, ICreateUserPresenter createUserPresenter)
        {
            _createUserUseCase = createUserUseCase;
            _createUserPresenter = createUserPresenter;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserModel model)
        {
            var createUserRequest = new CreateUserRequest(model.UserName, model.Password, model.Roles);
            await _createUserUseCase.Handle(createUserRequest, _createUserPresenter);

            return _createUserPresenter.ActionResult;
        }
    }
}
