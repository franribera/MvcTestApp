using System.Threading.Tasks;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users.Delete
{
    public sealed class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserRequest request, IOutputPort<Response<User>> outputPort)
        {
            var user = await _userRepository.Get(request.Id);

            if (user == null)
            {
                outputPort.Handle(Response<User>.Fail(new[] { "User does not exist." }));
            }
            else
            {
                await _userRepository.Delete(user);
                outputPort.Handle(Response<User>.Success(user, "User successfully deleted."));
            }
        }
    }
}
