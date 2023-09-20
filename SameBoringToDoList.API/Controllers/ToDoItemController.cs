using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.ToDoItems.Commands.AddToDoItem;
using SameBoringToDoList.Application.ToDoItems.Commands.DeleteToDoItem;
using SameBoringToDoList.Application.ToDoItems.Commands.UpdateToDoItem;
using SameBoringToDoList.Application.ToDoItems.Queries.GetItemsForToDoList;

namespace SameBoringToDoList.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todo/{toDoListId:guid}/item")]
    public class ToDoItemController: ApiController
    {
        public ToDoItemController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> AddToDo([FromRoute] Guid toDoListId, [FromBody] CreateToDoItemRequest createToDoItem, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var command = new AddToDoItemCommand(toDoListId, id, createToDoItem.Title, createToDoItem.Description);
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Created(CreateResourceLocationUrl(id), null) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetAllToDoItems([FromRoute] Guid toDoListId, CancellationToken cancellationToken)
        {
            var query = new GetItemsForToDoListQuery(toDoListId);
            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("{itemTitle}")]
        public async Task<IActionResult> DeleteItemByName([FromRoute] DeleteItemByNameRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteToDoItemCommand(request.ToDoListId, request.ItemTitle);
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPatch("{itemTitle}")]
        public async Task<IActionResult> UpdateToDoItem([FromRoute] Guid toDoListId, [FromRoute] string itemTitle, UpdateToDoItemRequest body, CancellationToken cancellationToken)
        {
            var command = new UpdateToDoItemCommand(
                toDoListId,
                itemTitle,
                body.Title,
                body.Description,
                body.IsDone
            );

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
