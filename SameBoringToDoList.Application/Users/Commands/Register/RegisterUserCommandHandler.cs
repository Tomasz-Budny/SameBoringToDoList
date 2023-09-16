using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Users.Commands.Register
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var id = UserId.Create(request.userId);
            if (id.IsFailure) return id.Error;

            var login = UserLogin.Create(request.Login);
            if (login.IsFailure) return login.Error;

            var credentialId = CredentialId.Create(Guid.NewGuid());
            if(credentialId.IsFailure) return credentialId.Error;

            var password = Password.Create(request.Password);
            if (password.IsFailure) return password.Error;

            var credential = new Credential(credentialId.Value, password.Value);

            var user = new User(id.Value, login.Value, credential);
            await _userRepository.AddAsync(user, cancellationToken);

            return Result.Success();
        }
    }
}
