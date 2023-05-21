using System;
using System.Text.Json;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services;

public class InvitationService
{
    private Dictionary<long, Invitation> _invitations;
    private readonly ILogger<InvitationService> _logger;

    public InvitationService(ILogger<InvitationService> logger)
	{
        _invitations = new Dictionary<long, Invitation>();
        _logger = logger;
    }

    public void AddInvitation(Invitation invitation)
    {
        _invitations.Add(invitation.Id, invitation);
    }

    public Invitation? GetInvitation(long id)
    {
        if (_invitations.TryGetValue(id, out Invitation? invitation))
        {
            return invitation;
        }
        else
        {
            return null;
        }
    }

    public Dictionary<long, Invitation> GetAllInvitations()
    {
        return _invitations;
    }

    public Invitation? DeserializeInvitation(JsonElement userJson)
    {
        try
        {
            long eventId = userJson.GetProperty("EventId").GetInt64();
            long donorId = userJson.GetProperty("DonorId").GetInt64();
            return new Invitation(eventId, donorId);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {}", e.Message);
            return null;
        }
    }
}

