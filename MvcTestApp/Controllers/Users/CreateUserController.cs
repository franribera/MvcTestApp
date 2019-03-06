using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Commands.Users.Create;
using MvcTestApp.Models.Users;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Controllers.Users
{
    [Route("api/users")]
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
        public async Task<IActionResult> Post([FromBody] CreateUserModel value)
        {
            var createUserRequest = new CreateUserRequest(value.UserName, value.Password, value.Roles);
            await _createUserUseCase.Handle(createUserRequest, _createUserPresenter);

            return _createUserPresenter.ActionResult;
        }
    }
}
