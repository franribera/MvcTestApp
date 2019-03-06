using System.Linq;
using System.Threading.Tasks;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public class AssertFields : UpdateAssertionStepBase
    {
        private readonly IUserRepository _userRepository;

        public AssertFields(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task<UpdateUserResponse> AssertForUpdateAction(UpdateUserRequest request)
        {
            var userToUpdate = await _userRepository.Get(request.Id);

            if (request.UserName != null) userToUpdate.SetName(new Name(request.UserName));
            if (request.Password != null) userToUpdate.SetPassword(new Password(request.Password));
            if (request.Roles != null) userToUpdate.SetRoles(request.Roles.Select(role => new Role(new Name(role))));

            await _userRepository.Update(userToUpdate);

            return UpdateUserResponse.Success(userToUpdate, "User successfully updated.");
        }
    }
}