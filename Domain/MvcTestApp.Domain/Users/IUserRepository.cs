using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvcTestApp.Domain.Infrastructure;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Get(Name username);
        Task<User> Get(Guid id);
        Task<List<User>> GetAll();
        Task Add(User user);
        Task Delete(User user);
        Task Update(User user);
    }
}