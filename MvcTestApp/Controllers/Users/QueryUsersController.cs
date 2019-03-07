using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Queries.Users;

namespace MvcTestApp.Controllers.Users
{
    [Route("api/users")]
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
            return new OkObjectResult(await _userQueries.GetUser(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _userQueries.GetUsers());
        }
    }
}