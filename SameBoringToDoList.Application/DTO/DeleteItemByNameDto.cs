namespace SameBoringToDoList.Application.DTO
{
    public record DeleteItemByNameDto(Guid ToDoListId, string ItemTitle);
}
