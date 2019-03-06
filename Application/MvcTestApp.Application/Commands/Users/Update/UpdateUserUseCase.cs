using System.Threading.Tasks;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserRequest request, IOutputPort<UpdateUserResponse> outputPort)
        {
            var userExistAssertion = new AssertUserExistsStep(_userRepository);
            var duplicateNameAssertion = new AssertUserNameDuplication(_userRepository);
            var fieldsAssertion = new AssertFields(_userRepository);

            userExistAssertion.SetSuccessor(duplicateNameAssertion);
            duplicateNameAssertion.SetSuccessor(fieldsAssertion);

            outputPort.Handle(await userExistAssertion.AssertForUpdate(request));
        }
    }
}