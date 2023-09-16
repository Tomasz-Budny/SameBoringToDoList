using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.Errors
{
    public static class DomainErrors
    {
        public static readonly Error UserIdIsEmpty = new Error("UserId.IsEmpty", "UserId can not be empty!");
        public static readonly Error UserLoginIsTooShort = new Error("UserLogin.TooShort", "User login is too short!");
        public static readonly Error UserLoginIsTooLong = new Error("UserLogin.TooLong", "User login is too long!");

        public static readonly Error CredentialIdIsEmpty = new Error("CredentialId.IsEmpty", "CredentialId can not be empty!");
        public static readonly Func<int, Error> PasswordIsTooShort = (int minPasswordLength) => new Error("Password.TooShort", $"Provided password is too short. Minimum password length: {minPasswordLength}");


        public static readonly Error AuthorIdIsEmpty = new Error("AuthorId.IsEmpty", "authorId can not be empty!");
        public static readonly Error ToDoListIdIsEmpty = new Error("ToDoList.IdIsEmpty", "ToDoList id can not be empty!");
        public static readonly Error ToDoItemIdIsEmpty = new Error("ToDoItem.IdIsEmpty", "ToDoItem id can not be empty!");
        public static readonly Func<int, Error> ToDoItemTitleIsTooLong = (int maxLength) => new Error("ToDoItem.TitleTooLong", $"ToDoItem title is too long! Maximum acceptable length is {maxLength}.");
        public static readonly Func<int, Error> ToDoItemDescriptionIsTooLong = (int maxLength) => new Error("ToDoItem.DescriptionTooLong", $"ToDoItem description is too long! Maximum acceptable length is {maxLength}.");
        public static readonly Func<int, Error> ToDoListTitleIsTooLong = (int maxLength) => new Error("ToDoList.TitleTooLong", $"ToDoList title is too long! Maximum acceptable length is {maxLength}.");
        public static readonly Func<string, ToDoListId, Error> ToDoItemWithTitleDoesNotExist = (string ToDoItem, ToDoListId ToDoListId) => new Error("ToDoList.ItemWithTitleDoesNotExist", $"ToDoItem with name {ToDoItem} does not exist on list with id: {ToDoListId}");
        public static readonly Func<ToDoItemId, ToDoListId, Error> ToDoItemWithIdDoesNotExist = (ToDoItemId ToDoItemId, ToDoListId ToDoListId) => new Error("ToDoList.ItemWithIdDoesNotExist", $"ToDoItem with id {ToDoItemId} does not exist on list with id: {ToDoListId}");
    }
}
