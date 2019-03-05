using System.Linq;
using System.Threading.Tasks;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public class UpdateUserUseCase : IInputPort<UpdateUserRequest, Response<User>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserRequest request, IOutputPort<Response<User>> outputPort)
        {
            var existingUser = await _userRepository.Get(request.Id);

            if (existingUser == null)
            {
                outputPort.Handle(Response<User>.Fail(new[] { "User does not exist." }));
            }
            else if (request.UserName != null && await _userRepository.Get(new Name(request.UserName)) != null)
            {
                outputPort.Handle(Response<User>.Fail(new[] { "The specified user name is already in use." }));
            }
            else
            {
                if (request.UserName != null) existingUser.SetName(new Name(request.UserName));
                if (request.Password != null) existingUser.SetPassword(new Password(request.Password));
                if (request.Roles != null) existingUser.SetRoles(request.Roles.Select(role => new Role(new Name(role))));

                await _userRepository.Update(existingUser);
                outputPort.Handle(Response<User>.Success(existingUser, "User successfully updated."));
            }
        }
    }
}