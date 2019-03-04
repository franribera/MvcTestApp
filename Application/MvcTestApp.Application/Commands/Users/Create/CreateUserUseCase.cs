using System.Threading.Tasks;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Application.Commands.Users.Create
{
    public sealed class CreateUserUseCase : IInputPort<CreateUserRequest, Response<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public CreateUserUseCase(IUserRepository userRepository, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        /// <inheritdoc />
        public async Task Handle(CreateUserRequest request, IOutputPort<Response<User>> outputPort)
        {
            var user = _userFactory.Create(request);
            var existingUser = await _userRepository.Get(user.UserName);

            if (existingUser == null)
            {
                await _userRepository.Add(user);

                outputPort.Handle(Response<User>.Success(user, "User successfully created."));
            }
            else
            {
                outputPort.Handle(Response<User>.Fail(new[]{"User already exists."}));
            }
        }
    }
}
