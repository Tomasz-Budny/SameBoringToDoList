namespace SameBoringToDoList.Application.DTO
{
    public static class Extensions
    {
        public static ToDoListDto AsDTO(this Domain.Entities.ToDoList obj)
        {
            return new(obj.Id.Value, obj.Title.Value, obj.Title.Value);
        }

        public static ToDoItemDto AsDTO(this Domain.Entities.ToDoItem obj)
        {
            return new(obj.Title.Value, obj.Description, obj.IsDone);
        }
    }
}
