using MvcTestApp.Application.Commands.Users;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands
{
    public interface IUserFactory
    {
        User Create(IUserRequest userRequest);
    }
}