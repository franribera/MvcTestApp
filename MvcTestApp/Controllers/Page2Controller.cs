using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Models.Users;

namespace MvcTestApp.Controllers
{
    [Authorize(Roles = "PAGE_2")]
    public class Page2Controller : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var userModel = new UserModel {UserName = HttpContext.User.Identity.Name};
            return View(userModel);
        }
    }
}