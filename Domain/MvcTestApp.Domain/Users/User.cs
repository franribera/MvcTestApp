using System;

namespace MvcTestApp.Domain.Users
{
    public class User
    {
        public string UserName { get; protected set; }

        public User(string userName)
        {
            SetUserNameCore(userName);
        }

        public void SetUserName(string userName)
        {
            SetUserNameCore(userName);
        }

        protected void SetUserNameCore(string userName)
        {
            if (userName.Trim() == string.Empty) throw new ArgumentException("UserName cannot be empty.", nameof(userName));

            UserName = userName;
        }
    }
}
