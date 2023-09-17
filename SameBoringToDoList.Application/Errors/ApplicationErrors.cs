using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Errors
{
    public static class ApplicationErrors
    {
        public static readonly Error ToDoListNotFound = new Error("ToDoList.NotFound", "to do list has not been found!");
        public static readonly Error PasswordIsInvalid = new Error("Password.Invalid", "Provided password is invalid!");

        public static readonly Error UserAlreadyExists = new Error("User.Exists", "User with provided login already exists!");
    }
}
