using System;

namespace MvcTestApp.Application.Commands.Users.Delete
{
    public class DeleteUserRequest
    {
        public Guid Id { get; }

        public DeleteUserRequest(Guid id)
        {
            Id = id;
        }
    }
}