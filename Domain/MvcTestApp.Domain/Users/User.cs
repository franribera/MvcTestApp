using System;

namespace MvcTestApp.Domain.Users
{
    public class User
    {
        public string UserName { get; protected set; }

        public void SetUserName(string userName)
        {
            if(userName.Trim() == string.Empty) throw new ArgumentException("UserName cannot be empty.", nameof(userName));

            UserName = userName;
        }
    }
}
