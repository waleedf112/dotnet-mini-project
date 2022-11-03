using System;
using Microsoft.AspNetCore.Mvc;
using Todos.Services;
using Todos.Models;
using MongoDB.Bson;

namespace Todos.Controllers;

[Controller]
[Route("api/[controller]")]
public class UserController : Controller
{

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{username}")]
    public async Task<User> Get(string username)
    {
        try
        {
            var user = await _userService.GetAsync(username);
            return user!;
        }
        catch (System.Exception)
        {
            var user = new User();
            user.username = username;
            await _createNewUser(user);
            return user;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user) => await _createNewUser(user);

    private async Task<CreatedAtActionResult> _createNewUser(User user)
    {
        await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

}