using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Queries.Users;
using MvcTestApp.Authentication;

namespace MvcTestApp.Controllers.Users
{
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    public class QueryUsersController : ControllerBase
    {
        private readonly IUserQueries _userQueries;

        public QueryUsersController(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userQueries.GetUser(id);

            if (user == null) return NotFound();
            return new OkObjectResult(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userQueries.GetUsers();

            if(users.Any()) return new OkObjectResult(users);
            return NoContent();
        }
    }
}