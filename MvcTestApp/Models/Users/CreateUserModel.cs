using System.Collections.Generic;

namespace MvcTestApp.Models.Users
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}