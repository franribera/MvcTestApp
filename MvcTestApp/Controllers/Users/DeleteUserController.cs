using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Commands.Users.Delete;
using MvcTestApp.Authentication;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Controllers.Users
{
    [Route("api/users")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    public class DeleteUserController : ControllerBase
    {
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly IDeleteUserPresenter _deleteUserPresenter;

        public DeleteUserController(IDeleteUserUseCase deleteUserUseCase, IDeleteUserPresenter deleteUserPresenter)
        {
            _deleteUserUseCase = deleteUserUseCase;
            _deleteUserPresenter = deleteUserPresenter;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteUserRequest = new DeleteUserRequest(id);
            await _deleteUserUseCase.Handle(deleteUserRequest, _deleteUserPresenter);

            return _deleteUserPresenter.ActionResult;
        }
    }
}
