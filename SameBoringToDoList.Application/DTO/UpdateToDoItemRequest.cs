namespace SameBoringToDoList.Application.DTO
{
    public record UpdateToDoItemRequest(
        string? Title = null,
        string? Description = null,
        bool? IsDone = null);
}
