using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users
{
    public interface IUserFactory
    {
        User Create(IUserRequest userRequest);
    }
}