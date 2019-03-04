using System.Linq;
using System.Threading.Tasks;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Application.Commands.Users.Create
{
    public sealed class CreateUserUseCase : IInputPort<CreateUserRequest, Response<User>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task Handle(CreateUserRequest request, IOutputPort<Response<User>> outputPort)
        {
            var userName = new Name(request.UserName);
            var user = await _userRepository.Get(userName);

            if (user == null)
            {
                var password = new Password(request.Password);
                var newUser = new User(userName, password);
                request.Roles.ToList().ForEach(role => newUser.AddRole(new Role(new Name(role))));
                await _userRepository.Add(newUser);

                outputPort.Handle(Response<User>.Success(newUser, "User successfully created."));
            }
            else
            {
                outputPort.Handle(Response<User>.Fail(new[]{"User already exists."}));
            }
        }
    }
}
