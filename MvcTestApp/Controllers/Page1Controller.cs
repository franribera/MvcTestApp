using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Models.Users;

namespace MvcTestApp.Controllers
{
    [Authorize(Roles = "ADMIN, PAGE_1")]
    public class Page1Controller : Controller
    {
        // GET
        public IActionResult Index()
        {
            var userModel = new UserModel {UserName = HttpContext.User.Identity.Name};
            return View(userModel);
        }
    }
}