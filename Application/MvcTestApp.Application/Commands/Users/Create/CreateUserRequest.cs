using System;
using System.Collections.Generic;

namespace MvcTestApp.Application.Commands.Users.Create
{
    public class CreateUserRequest
    {
        public string UserName { get; }
        public string Password { get; }
        public IEnumerable<string> Roles { get; }

        public CreateUserRequest(string userName, string password, IEnumerable<string> roles)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }
    }
}