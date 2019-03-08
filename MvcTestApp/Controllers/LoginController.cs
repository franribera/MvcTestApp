using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login()
        {
            var loginModel = new LoginModel {RefererUrl = Request.Headers["Referer"].ToString()};
            return View(loginModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var claimsPrincipal = await _authenticationService.Login(loginModel.UserName, loginModel.Password);

            if (claimsPrincipal == null) return View(loginModel);
                
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Redirect(loginModel.RefererUrl);
        }
    }
}