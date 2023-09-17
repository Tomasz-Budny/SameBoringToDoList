namespace SameBoringToDoList.Application.DTO
{
    public record ToDoListWithItemsDto(Guid Id, string Title, IEnumerable<ToDoItemDto> Items);
}
