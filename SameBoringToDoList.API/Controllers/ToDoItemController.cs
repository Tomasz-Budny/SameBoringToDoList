﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.ToDoItem.Commands.AddToDoItem;
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
        public async Task<IActionResult> AddToDo([FromRoute] Guid toDoListId,[FromBody] CreateToDoItemDto createToDoItem)
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
    }
}
