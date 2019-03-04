using System.Threading.Tasks;
using MvcTestApp.Domain.Infrastructure;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Get(Name username);
        Task Add(User user);
        Task Delete(User user);
    }
}