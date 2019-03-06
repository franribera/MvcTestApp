using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users.Delete
{
    public interface IDeleteUserUseCase : IInputPort<DeleteUserRequest, Response<User>>
    {
        
    }
}