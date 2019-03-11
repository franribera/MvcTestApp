    using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ClaimsPrincipal> Login(string userName, string password)
        {
            var user = await _userRepository.Get(new Name(userName));

            if (user == null || user.Password != new Password(password)) return null;

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name.Value)));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}