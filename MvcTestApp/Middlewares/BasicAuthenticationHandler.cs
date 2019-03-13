using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using IAuthenticationService = MvcTestApp.Authentication.IAuthenticationService;

namespace MvcTestApp.Middlewares
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthenticationService _authenticationService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticationService authenticationService)
            : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
                return AuthenticateResult.Fail("Missing Authorization Header");

            ClaimsPrincipal claimsPrincipal;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[HeaderNames.Authorization]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                claimsPrincipal = await _authenticationService.Login(username, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (claimsPrincipal == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}