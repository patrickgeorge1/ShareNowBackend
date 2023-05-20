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
    private readonly EventService _eventsService;
    private readonly InvitationService _invitationService;
    private readonly RequestService _requestService;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, UserService userService, EventService eventsService, InvitationService invitationService, RequestService requestService)
    {
        _logger = logger;
        _userService = userService;
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public IActionResult GetUserById(long id)
    {
        _logger.LogInformation("GetUser {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        User? user = _userService.GetUser(id);
        if (user != null)
        {
            return Ok(user);
        }

        return NotFound();
    }

    // POST: api/User
    [HttpPost]
    public IActionResult CreateUser(JsonElement userJson)
    {
        _logger.LogInformation("CreateUser {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        User? newUser = _userService.DeserializeUser(userJson);
        if (newUser != null)
        {
            _userService.AddUser(newUser);
            return Ok(newUser);
        }
        return BadRequest("Invalid input");            
    }
}
