using System.Threading.Tasks;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public class AssertUserExistsStep : UpdateAssertionStepBase
    {
        private readonly IUserRepository _userRepository;

        public AssertUserExistsStep(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task<UpdateUserResponse> AssertForUpdateAction(UpdateUserRequest request)
        {
            var userToUpdate = await _userRepository.Get(request.Id);

            return userToUpdate == null ? UpdateUserResponse.UserNotFound(new[] {"User does not exist."}) : null;
        }
    }
}