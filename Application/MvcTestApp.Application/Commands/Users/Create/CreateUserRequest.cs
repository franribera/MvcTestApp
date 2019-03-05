using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTestApp.Application.Commands.Users.Create
{
    public class CreateUserRequest : IUserRequest
    {
        public string UserName { get; }
        public string Password { get; }
        public IEnumerable<string> Roles { get; }

        public CreateUserRequest(string userName, string password, IEnumerable<string> roles)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName), "UserName not set.");
            Password = password ?? throw new ArgumentNullException(nameof(password), "Password not set.");
            Roles = AssertRoles(roles);
        }

        private IEnumerable<string> AssertRoles(IEnumerable<string> roles)
        {
            if(roles == null) throw new ArgumentNullException(nameof(roles), "Roles not set.");
            var rolesList = roles.ToList();
            if(!rolesList.Any()) throw new ArgumentException( "User must has 1 role at least.", nameof(roles));
            if(rolesList.Count != rolesList.Distinct().Count()) throw new ArgumentException("User can't has duplicated roles.", nameof(roles));

            return rolesList;
        }
    }
}