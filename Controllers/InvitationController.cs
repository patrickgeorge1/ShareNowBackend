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
    public IActionResult GetInvitationById(string id)
    {
        _logger.LogInformation("GetInvitation {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Invitation? invitation = _invitationService.GetInvitation(id);
        if (invitation != null)
        {
            return Ok(invitation);
        }

        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAllInvitations()
    {
        _logger.LogInformation("GetAllInvitations {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Invitation> invitations = _invitationService.GetAllInvitations().Values.ToList();
        return Ok(invitations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvitation(JsonElement invitationJson)
    {
        _logger.LogInformation("CreateInvitation {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Invitation? invitation = _invitationService.DeserializeInvitation(invitationJson);
        if (invitation != null)
        {
            return Ok(await _invitationService.AddInvitation(invitation));
        }
        return BadRequest("Invalid input");
    }
}
