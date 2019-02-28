using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Users
{
    public class User
    {
        public Name UserName { get; protected set; }

        public User(Name userName)
        {
            UserName = userName;
        }

        public void SetUserName(Name userName)
        {
            UserName = userName;
        }
    }
}
