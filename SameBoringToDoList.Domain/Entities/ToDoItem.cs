using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Domain.Primitives;

namespace SameBoringToDoList.Domain.Entities
{
    public class ToDoItem: Entity<ToDoItemId>
    {
        public ToDoItemTitle Title { get; set; }
        public ToDoItemDescription Description { get; set; }
        public bool IsDone { get; set; }
        public ToDoItem(ToDoItemId id, ToDoItemTitle title, ToDoItemDescription description, bool isDone): base(id)
        {
            Title = title;
            Description = description;
            IsDone = isDone;
        }

        public void MarkAsDone()
        {
            IsDone = true;
        }
    }
}
