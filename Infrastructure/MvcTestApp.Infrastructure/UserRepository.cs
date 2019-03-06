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
        private static readonly ConcurrentDictionary<Guid, User> Users = new ConcurrentDictionary<Guid, User>(new List<KeyValuePair<Guid, User>>()
        {
            new KeyValuePair<Guid, User>(User.User1.Id, User.User1),
            new KeyValuePair<Guid, User>(User.User2.Id, User.User2),
            new KeyValuePair<Guid, User>(User.User3.Id, User.User3)
        });

        public Task<User> Get(Name username)
        {
            return Task.FromResult(Users.SingleOrDefault(user => user.Value.UserName == username).Value);
        }

        public Task<User> Get(Guid id)
        {
            return Task.FromResult(Users.SingleOrDefault(user => user.Key == id).Value);
        }

        public Task Add(User user)
        {
            // I'm assuming it never fails
            return Task.FromResult(Users.TryAdd(user.Id, user));
        }

        public Task Delete(User user)
        {
            // I'm assuming it never fails
            return Task.FromResult(Users.TryRemove(user.Id, out var removedUser));
        }

        public Task Update(User user)
        {
            // I'm assuming it never fails
            if (Users.TryGetValue(user.Id, out var currentUser))
            {
                return Task.FromResult(Users.TryUpdate(user.Id, user, currentUser));
            }

            throw new KeyNotFoundException($"The user {user.UserName.Value} was not found.");
        }
    }
}
