using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : IUserRepository
    {
        private static readonly ConcurrentDictionary<Name, User> Users = new ConcurrentDictionary<Name, User>(new List<KeyValuePair<Name, User>>()
        {
            new KeyValuePair<Name, User>(User.User1.UserName, User.User1),
            new KeyValuePair<Name, User>(User.User2.UserName, User.User2),
            new KeyValuePair<Name, User>(User.User3.UserName, User.User3)
        });

        public Task<User> Get(Name username)
        {
            return Task.FromResult(Users.SingleOrDefault(user => user.Key == username).Value);
        }

        public Task<User> Get(Guid id)
        {
            return Task.FromResult(Users.SingleOrDefault(user => user.Value.Id == id).Value);
        }

        public Task Add(User user)
        {
            // I'm assuming it never fails
            return Task.FromResult(Users.TryAdd(user.UserName, user));
        }

        public Task Delete(User user)
        {
            // I'm assuming it never fails
            return Task.FromResult(Users.TryRemove(user.UserName, out var removedUser));
        }

        public Task Update(User user)
        {
            // I'm assuming it never fails
            if (Users.TryGetValue(user.UserName, out var currentUser))
            {
                return Task.FromResult(Users.TryUpdate(user.UserName, user, currentUser));
            }

            throw new KeyNotFoundException($"The user {user.UserName.Value} was not found.");
        }
    }
}
