namespace SameBoringToDoList.Application.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId);
    }
}
