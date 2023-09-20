using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Application.DTO
{
    public static class Extensions
    {
        public static ToDoListDto AsDto(this ToDoList obj)
        {
            return new(obj.Id.Value, obj.Title.Value);
        }

        public static ToDoItemDto AsDto(this ToDoItem obj)
        {
            return new(obj.Title.Value, obj.Description, obj.IsDone);
        }

        public static ToDoListWithItemsDto AsDtoWithItems(this ToDoList obj)
        {
            var items = obj.ToDoItems.Select(i => i.AsDto());

            return new(obj.Id.Value, obj.Title.Value, items);
        }
    }
}
