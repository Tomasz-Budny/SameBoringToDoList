using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Authentication;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Users.Queries.Login
{
    public class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwt;
        private readonly ISmtpService _smtp;

        public LoginUserQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwt, ISmtpService smtpService)
        {
            _userRepository = userRepository;
            _jwt = jwt;
            _smtp = smtpService;
        }

        public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var login = UserLogin.Create(request.login);
            if (login.IsFailure) return login.Error;

            var user = await _userRepository.GetByLoginWithCredentialsAsync(login.Value, cancellationToken);

            var isPasswordInvalid = !user.Credential.ValidatePassword(request.password);
            if (isPasswordInvalid) return ApplicationErrors.PasswordIsInvalid;

            _smtp.SendConfirmationEmail("tamia.bergnaum0@ethereal.email");

            var token = _jwt.GenerateToken(user.Id.Value);
            return token;
        }
    }
}
