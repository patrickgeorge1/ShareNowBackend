using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareNowBackend.Services;

namespace ShareNowBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvitationController : ControllerBase
{
    private readonly InvitationService _invitationService;
    private readonly ILogger<InvitationController> _logger;

    public InvitationController(InvitationService invitationService, ILogger<InvitationController> logger)
    {
        _invitationService = invitationService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult GetInvitationById(long id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateInvitation(JsonElement invitationJson)
    {
        return Ok();
    }
}
