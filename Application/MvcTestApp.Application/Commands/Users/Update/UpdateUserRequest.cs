using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public class UpdateUserRequest : IUserRequest
    {
        public Guid Id { get; }
        public string UserName { get; }
        public string Password { get; }
        public IEnumerable<string> Roles { get; }

        public UpdateUserRequest(Guid id, string userName, string password, IEnumerable<string> roles)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid user id.", nameof(id));
            Id = id;
            UserName = userName;
            Password = password;
            Roles = roles;
        }
    }
}
