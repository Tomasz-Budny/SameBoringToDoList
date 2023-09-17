using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList;
using SameBoringToDoList.Application.ToDoList.Queries.GetAllToDoListsForUser;
using SameBoringToDoList.Application.ToDoList.Queries.GetToDoListById;

namespace SameBoringToDoList.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todo")]
    public class ToDoListController : ApiController
    {
        public ToDoListController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListDto request)
        {
            var authorId = GetSenderId();
            var id = Guid.NewGuid();

            var command = new CreateToDoListCommand(authorId, id, request.Title);
            var result = await _sender.Send(command);

            return result.IsSuccess ? Created(CreatedResourceLocation(id), null): BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetToDoList([FromRoute] GetToDoListByIdDto request)
        {
            var senderId = GetSenderId();
            var query = new GetToDoListByIdQuery(request.Id, senderId);
            var result = await _sender.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToDoLists()
        {
            var senderId = GetSenderId();
            var query = new GetAllToDoListsForUserQuery(senderId);
            var result = await _sender.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
