using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcTestApp.Application.DTOs;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Queries.Users
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            var user = await _userRepository.Get(id);
            return user != null ? MapUSerToUserDto(user) : null;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAll();
            return users.Any() ? users.Select(MapUSerToUserDto).ToList() : new List<UserDto>();
        }

        private static UserDto MapUSerToUserDto(User user)
        {
            return new UserDto
            {
                UserName = user.UserName.Value,
                Id = user.Id,
                Roles = user.Roles.Select(role => role.Name.Value)
            };
        }

    }
}