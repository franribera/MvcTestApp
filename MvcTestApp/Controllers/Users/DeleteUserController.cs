using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Commands.Users.Delete;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Controllers.Users
{
    [Route("api/users")]
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

        [HttpDelete("{userName}")]
        public async Task<IActionResult> Post(string userName)
        {
            var deleteUserRequest = new DeleteUserRequest(userName);
            await _deleteUserUseCase.Handle(deleteUserRequest, _deleteUserPresenter);

            return _deleteUserPresenter.ActionResult;
        }
    }
}
