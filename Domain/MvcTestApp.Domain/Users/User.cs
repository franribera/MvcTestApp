using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Users
{
    public class User
    {
        public Name UserName { get; protected set; }
        public Password Password { get; protected set; }

        public User(Name userName, Password password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
