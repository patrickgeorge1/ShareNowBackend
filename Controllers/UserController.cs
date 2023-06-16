using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShareNowBackend.Services;
using System.Text.Json;
using ShareNowBackend.Models;
using Microsoft.Extensions.Logging;

namespace ShareNowBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(UserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }


    // GET: api/User/5
    [HttpGet("{id}")]
    public IActionResult GetUserById(string id)
    {
        _logger.LogInformation("GetUser {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        User? user = _userService.GetUser(id);
        if (user != null)
        {
            return Ok(user);
        }

        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAllUsers(long id)
    {
        _logger.LogInformation("GetAllUsers {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<User> users = _userService.GetAllUsers().Values.ToList();
        return Ok(users);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> CreateUser(JsonElement userJson)
    {
        _logger.LogInformation("CreateUser {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        User? newUser = _userService.DeserializeUser(userJson);
        if (newUser != null)
        {
            var resp = await _userService.AddUser(newUser);
            return Ok(resp);
        }
        return BadRequest("Invalid input");            
    }
}
