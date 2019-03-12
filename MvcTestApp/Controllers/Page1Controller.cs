using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Models.Users;

namespace MvcTestApp.Controllers
{
    [Authorize(Roles = "PAGE_1")]
    public class Page1Controller : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var userModel = new UserModel {UserName = HttpContext.User.Identity.Name};
            return View(userModel);
        }
    }
}