using System;
using System.Collections.Generic;

namespace MvcTestApp.Models.Users
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}
