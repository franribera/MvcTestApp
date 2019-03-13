using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Domain.Users;
using MvcTestApp.Models.Users;
using IAuthenticationService = MvcTestApp.Authentication.IAuthenticationService;
using LoginModel = MvcTestApp.Models.Authentication.LoginModel;

namespace MvcTestApp.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            return View(new LoginModel{ ReturnUrl = returnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var claimsPrincipal = await _authenticationService.Login(loginModel.UserName, loginModel.Password);

            if (claimsPrincipal == null) return View(loginModel);
                
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            var redirectionView = string.IsNullOrWhiteSpace(loginModel.ReturnUrl)
                ? ResolveHomePageFromRole(claimsPrincipal)
                : loginModel.ReturnUrl;

            return Redirect(redirectionView);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public IActionResult Unauthorize()
        {
            return View(new UserModel
            {
                UserName = HttpContext.User.Identity.Name
            });
        }

        private static string ResolveHomePageFromRole(IPrincipal claimsPrincipal)
        {
            // This breaks the OCP

            if (claimsPrincipal.IsInRole(role: Role.PAGE_2.Name.Value))
                return "/Page2";

            if (claimsPrincipal.IsInRole(role: Role.PAGE_3.Name.Value))
                return "/Page3";

            return "/Page1";
        }
    }
}