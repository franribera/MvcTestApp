using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvcTestApp.Application.DTOs;

namespace MvcTestApp.Application.Queries.Users
{
    public interface IUserQueries
    {
        Task<UserDto> GetUser(Guid id);
        Task<List<UserDto>> GetUsers();
    }
}
