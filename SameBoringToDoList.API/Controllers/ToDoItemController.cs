using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.ToDoItem.Commands.AddToDoItem;
using SameBoringToDoList.Application.ToDoItem.Commands.DeleteToDoItem;
using SameBoringToDoList.Application.ToDoItem.Commands.UpdateToDoItem;
using SameBoringToDoList.Application.ToDoItem.Queries.GetItemsForToDoList;

namespace SameBoringToDoList.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todo/{toDoListId:guid}/item")]
    public class ToDoItemController: ApiController
    {
        public ToDoItemController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> AddToDo([FromRoute] Guid toDoListId, [FromBody] CreateToDoItemRequest createToDoItem)
        {
            var id = Guid.NewGuid();
            var command = new AddToDoItemCommand(toDoListId, id, createToDoItem.Title, createToDoItem.Description);
            var result = await _sender.Send(command);

            return result.IsSuccess ? Created(CreateResourceLocationUrl(id), null) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToDoItems([FromRoute] Guid toDoListId)
        {
            var query = new GetItemsForToDoListQuery(toDoListId);
            var result = await _sender.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("{itemTitle}")]
        public async Task<IActionResult> DeleteItemByName([FromRoute] DeleteItemByNameRequest request)
        {
            var command = new DeleteToDoItemCommand(request.ToDoListId, request.ItemTitle);
            var result = await _sender.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPatch("{itemTitle}")]
        public async Task<IActionResult> UpdateToDoItem([FromRoute] Guid toDoListId, [FromRoute] string itemTitle, UpdateToDoItemRequest body)
        {
            var command = new UpdateToDoItemCommand(
                toDoListId,
                itemTitle,
                body.Title,
                body.Description,
                body.IsDone
            );

            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
