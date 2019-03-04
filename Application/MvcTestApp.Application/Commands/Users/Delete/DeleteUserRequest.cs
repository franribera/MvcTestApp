using System;

namespace MvcTestApp.Application.Commands.Users.Delete
{
    public class DeleteUserRequest
    {
        public string UserName { get; }

        public DeleteUserRequest(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName), "UserName not set.");
        }
    }
}