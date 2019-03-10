using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Commands.Users.Update;
using MvcTestApp.Models.Users;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Controllers.Users
{
    [Route("api/users")]
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    public class UpdateUserController : ControllerBase
    {
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IUpdateUserPresenter _updateUserPresenter;

        public UpdateUserController(IUpdateUserUseCase updateUserUseCase, IUpdateUserPresenter updateUserPresenter)
        {
            _updateUserUseCase = updateUserUseCase;
            _updateUserPresenter = updateUserPresenter;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CreateUserModel model)
        {
            var request = new UpdateUserRequest(id, model.UserName, model.Password, model.Roles);
            await _updateUserUseCase.Handle(request, _updateUserPresenter);

            return _updateUserPresenter.ActionResult;
        }
    }
}