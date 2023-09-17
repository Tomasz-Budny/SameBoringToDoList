using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService<Guid> _userContext;

        public ChangePasswordCommandHandler(IUserRepository userRepository, IUserContextService<Guid> userContextService)
        {
            _userRepository = userRepository;
            _userContext = userContextService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(_userContext.GetUserId);
            if (userId.IsFailure) return userId.Error;

            var user = await _userRepository.GetWithCredentialsAsync(userId, cancellationToken);

            var passwordIsInvalid = !user.Credential.ValidatePassword(request.CurrentPassword);
            if (passwordIsInvalid) return ApplicationErrors.PasswordIsInvalid;

            var newPassword = Password.Create(request.NewPassword);
            if (newPassword.IsFailure) return newPassword.Error;

            var newCredentials = Credential.Create(newPassword);
            user.Credential = newCredentials;
            await _userRepository.UpdateAsync(user, cancellationToken);

            return Result.Success();
        }
    }
}
