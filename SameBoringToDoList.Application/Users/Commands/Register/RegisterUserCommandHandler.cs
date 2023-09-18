using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Users.Commands.Register
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISmtpService _smtp;

        public RegisterUserCommandHandler(IUserRepository userRepository, ISmtpService smtpService)
        {
            _userRepository = userRepository;
            _smtp = smtpService;
        }
        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var id = UserId.Create(request.UserId);
            if (id.IsFailure) return id.Error;

            var email = Email.Create(request.Email);
            if (email.IsFailure) return email.Error;

            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user is not null) return ApplicationErrors.UserAlreadyExists;

            var password = Password.Create(request.Password);
            if (password.IsFailure) return password.Error;

            var credential = Credential.Create(password);
            var verificationToken = credential.Value.VerificationToken;

            _smtp.SendConfirmationEmail(email, verificationToken);

            var newUser = new User(id, email, credential);
            await _userRepository.AddAsync(newUser, cancellationToken);

            return Result.Success();
        }
    }
}
