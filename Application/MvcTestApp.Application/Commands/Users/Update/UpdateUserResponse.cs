using System.Collections.Generic;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public class UpdateUserResponse
    {
        public bool NotFound { get; }
        public bool DuplicatedName { get; }
        public bool SuccessFul { get; }
        public string Message { get; }
        public User User { get; }
        public IEnumerable<string> Errors { get; }

        protected UpdateUserResponse(bool successful, bool notFound, bool duplicatedName, User user, string message, IEnumerable<string> errors)
        {
            SuccessFul = successful;
            NotFound = notFound;
            DuplicatedName = duplicatedName;
            Message = message;
            User = user;
            Errors = errors;
        }

        public static UpdateUserResponse Success(User user, string message = null)
        {
            return new UpdateUserResponse(true, false, false, user, message, new List<string>());
        }

        public static UpdateUserResponse UserNotFound(IEnumerable<string> errors)
        {
            return new UpdateUserResponse(false, true, false, null, null, errors);
        }

        public static UpdateUserResponse DuplicatedUserName(IEnumerable<string> errors)
        {
            return new UpdateUserResponse(false, false, true, null, null, errors);
        }
    }
}