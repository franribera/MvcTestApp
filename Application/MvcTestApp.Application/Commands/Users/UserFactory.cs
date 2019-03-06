using System.Linq;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Commands.Users
{
    public class UserFactory : IUserFactory
    {
        /// <inheritdoc />
        public User Create(IUserRequest userRequest)
        {
            var userName = new Name(userRequest.UserName);
            var password = new Password(userRequest.Password);
            var user = new User(userName, password, userRequest.Roles.Select(role => new Role(new Name(role))));

            return user;
        }
    }
}