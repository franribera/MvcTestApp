using System;
using System.Collections.Generic;
using System.Text;

namespace MvcTestApp.Domain.Users
{
    public class User
    {
        public string UserName { get; protected set; }

        public void SetUserName(string userName)
        {
            UserName = userName;
        }
    }
}
