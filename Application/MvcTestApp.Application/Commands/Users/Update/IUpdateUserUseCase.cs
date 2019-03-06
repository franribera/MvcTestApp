using MvcTestApp.Application.Infrastructure;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public interface IUpdateUserUseCase : IInputPort<UpdateUserRequest, UpdateUserResponse>
    {
    }
}