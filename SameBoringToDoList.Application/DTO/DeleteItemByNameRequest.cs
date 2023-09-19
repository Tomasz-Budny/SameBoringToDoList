namespace SameBoringToDoList.Application.DTO
{
    public record DeleteItemByNameRequest(Guid ToDoListId, string ItemTitle);
}
