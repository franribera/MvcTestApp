using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users.Create
{
    public interface ICreateUserUseCase : IInputPort<CreateUserRequest, Response<User>>
    {
    }
}
