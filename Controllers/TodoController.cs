using Microsoft.AspNetCore.Mvc;
using Todos.Services;
using Todos.Models;
using MongoDB.Bson;

namespace Todos.Controllers;



[Controller]
[Route("api/[controller]")]
public class TodoController : Controller
{

    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet("{todoId}")]
    public async Task<Todo> Get(string todoId)
    {
        return await _todoService.GetAsync(todoId);
    }

    [HttpGet]
    [Route("list/{userId}")]
    public async Task<List<Todo>> GetAll(string userId)
    {
        return await _todoService.GetAllAsync(userId);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Todo todo)
    {
        await _todoService.CreateAsync(todo);
        return CreatedAtAction(nameof(Post), new { todoId = todo.Id }, todo);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Todo todo)
    {
        await _todoService.UpdateAsync(todo);
        return CreatedAtAction(nameof(Put), new { todoId = todo.Id }, todo);
    }

    [HttpDelete("{todoId}")]
    public async Task<IActionResult> Delete(string todoId)
    {
        await _todoService.DeleteAsync(todoId);
        return AcceptedAtAction(nameof(Delete));
    }
}