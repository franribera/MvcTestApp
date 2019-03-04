using System.Collections.Generic;

namespace MvcTestApp.Application.Commands.Users
{
    public interface IUserRequest
    {
        string UserName { get; }
        string Password { get; }
        IEnumerable<string> Roles { get; }
    }
}