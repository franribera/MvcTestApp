using System;

namespace MvcTestApp.Domain.Users
{
    public class User
    {
        public string UserName { get; protected set; }

        public void SetUserName(string userName)
        {
            if(userName == string.Empty) throw new ArgumentException("UserName cannot be empty.", nameof(userName));
            if(userName == " ") throw new ArgumentException("UserName cannot be white space.", nameof(userName));

            UserName = userName;
        }
    }
}
