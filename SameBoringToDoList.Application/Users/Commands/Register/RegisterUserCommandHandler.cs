using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
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

            var user = await _userRepository.GetByLoginAsync(login.Value, cancellationToken);
            if (user is not null) return ApplicationErrors.UserAlreadyExists;

            var password = Password.Create(request.Password);
            if (password.IsFailure) return password.Error;

            var credential = Credential.Create(password);

            var nwUser = new User(id, login, credential);
            await _userRepository.AddAsync(nwUser, cancellationToken);

            return Result.Success();
        }
    }
}
