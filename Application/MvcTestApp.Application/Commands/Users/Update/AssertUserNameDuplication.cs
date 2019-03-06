using System.Threading.Tasks;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public class AssertUserNameDuplication : UpdateAssertionStepBase
    {
        private readonly IUserRepository _userRepository;

        public AssertUserNameDuplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task<UpdateUserResponse> AssertForUpdateAction(UpdateUserRequest request)
        {
            if (request.UserName != null)
            {
                var userWithDuplicatedUserName = await _userRepository.Get(new Name(request.UserName));

                if (userWithDuplicatedUserName != null && userWithDuplicatedUserName.Id != request.Id)
                    return UpdateUserResponse.DuplicatedUserName(new[] { "The specified user name is already in use." });
            }

            return null;
        }
    }
}