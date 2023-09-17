using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Domain.Events;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Domain.Primitives;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.Entities
{
    public class ToDoList: AggregateRoot<ToDoListId>
    {
        public ToDoListTitle Title { get; set; }
        public UserId AuthorId { get; set; }

        protected List<ToDoItem> _toDoItems = new List<ToDoItem>();
        public IReadOnlyList<ToDoItem> ToDoItems => _toDoItems.AsReadOnly();

        public ToDoList(ToDoListId Id, ToDoListTitle title, UserId authorId): base(Id) 
        {
            Title = title;
            AuthorId = authorId;
        }

        public Result Add(ToDoItem toDoItem)
        {
            var alreadyExists = _toDoItems.Any(x => x.Title == toDoItem.Title);
            if (alreadyExists) return DomainErrors.ToDoItemWithTitleExists;

            _toDoItems.Add(toDoItem);
            AddEvent(new ToDoItemAdded(this, toDoItem));

            return Result.Success();
        }

        public Result Remove(ToDoItem toDoItem)
        {
            if(!_toDoItems.Contains(toDoItem))
                return DomainErrors.ToDoItemWithTitleDoesNotExist(toDoItem.Title, this.Id);

            _toDoItems.Remove(toDoItem);
            AddEvent(new ToDoItemRemoved(this, toDoItem));

            return Result.Success();
        }

        public Result MarkAsDone(string toDoItemTitle)
        {
            var toDoItem = _toDoItems.SingleOrDefault(todo =>  todo.Title == toDoItemTitle);
            if(toDoItem == null)
                return DomainErrors.ToDoItemWithTitleDoesNotExist(toDoItemTitle, this.Id);

            toDoItem.MarkAsDone();
            AddEvent(new ToDoItemIsDone(this, toDoItem));

            return Result.Success();
        }

        public Result Update(ToDoItemId toDoItemId, ToDoItem updatedItem)
        {
            var toDoItem = _toDoItems.SingleOrDefault(todo => todo.Id == toDoItemId);
            if (toDoItem == null)
                return DomainErrors.ToDoItemWithIdDoesNotExist(toDoItemId, this.Id);
            
            toDoItem = updatedItem;
            AddEvent(new ToDoItemUpdated(this, toDoItem));

            return Result.Success();
        }
    }
}
