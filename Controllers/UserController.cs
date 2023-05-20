using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShareNowBackend.Services.EventServices;
using ShareNowBackend.Services.InvitationServices;
using ShareNowBackend.Services.RequestServices;
using ShareNowBackend.Services.UserServices;
using ShareNowBackend.Services;
using System.Text.Json;
using ShareNowBackend.Models;
using Microsoft.Extensions.Logging;

namespace ShareNowBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IEventService _eventsService;
        private readonly IInvitationService _invitationService;
        private readonly IRequestService _requestService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserService userService, IEventService eventsService, IInvitationService invitationService, IRequestService requestService)
        {
            _logger = logger;
            _userService = userService;
            _eventsService = eventsService;
            _invitationService = invitationService;
            _requestService = requestService;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(long id)
        {
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
            User? newUser = DeserializeUser(userJson);
            if (newUser != null)
            {
                _userService.AddUser(newUser);
                return Ok(newUser);
            }
            return BadRequest("Invalid input");            
        }



        private User? DeserializeUser(JsonElement userJson)
        {
            try
            {
                string name = userJson.GetProperty("Name").GetString();
                return new User(name);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
