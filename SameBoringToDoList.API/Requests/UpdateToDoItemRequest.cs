namespace SameBoringToDoList.API.Requests
{
    public record UpdateToDoItemRequest(
        string? Title = null,
        string? Description = null,
        bool? IsDone = null);
}
