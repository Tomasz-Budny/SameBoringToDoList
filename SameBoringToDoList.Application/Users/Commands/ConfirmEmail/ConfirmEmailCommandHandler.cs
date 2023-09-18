using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
    {
        private readonly IUserRepository _userRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByVerificationTokenAsync(request.verificationToken, cancellationToken);
            if (user == null) return ApplicationErrors.UserNotFound;

            user.Credential.VerifiedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user, cancellationToken);
            return Result.Success();
        }
    }
}
