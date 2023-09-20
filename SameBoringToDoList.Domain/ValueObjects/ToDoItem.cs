namespace SameBoringToDoList.Domain.ValueObjects
{
    public record ToDoItem
    {
        public ToDoItemTitle Title { get; set; }
        public ToDoItemDescription Description { get; set; }
        public bool IsDone { get; set; }
        public ToDoItem(ToDoItemTitle title, ToDoItemDescription description, bool isDone)
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
