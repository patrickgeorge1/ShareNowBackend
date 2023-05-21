using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareNowBackend.Models;
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
        _logger.LogInformation("GetInvitation {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Invitation? invitation = _invitationService.GetInvitation(id);
        if (invitation != null)
        {
            return Ok(invitation);
        }

        return NotFound();
    }

    [HttpPost]
    public IActionResult CreateInvitation(JsonElement invitationJson)
    {
        _logger.LogInformation("CreateInvitation {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Invitation? invitation = _invitationService.DeserializeInvitation(invitationJson);
        if (invitation != null)
        {
            _invitationService.AddInvitation(invitation);
            return Ok(invitation);
        }
        return BadRequest("Invalid input");
    }
}
