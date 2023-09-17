namespace SameBoringToDoList.Application.DTO
{
    public static class Extensions
    {
        public static ToDoListDto AsDTO(this Domain.Entities.ToDoList obj)
        {
            return new(obj.Id.Value, obj.Title.Value);
        }

        public static ToDoItemDto AsDTO(this Domain.Entities.ToDoItem obj)
        {
            return new(obj.Title.Value, obj.Description, obj.IsDone);
        }

        public static ToDoListWithItemsDto AsDtoWithItems(this Domain.Entities.ToDoList obj)
        {
            var items = obj.ToDoItems.Select(i => i.AsDTO());

            return new(obj.Id.Value, obj.Title.Value, items);
        }
    }
}
