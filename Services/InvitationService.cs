using System;
using System.Text.Json;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;
using ShareNowBackend.Repositories;

namespace ShareNowBackend.Services;

public class InvitationService
{
    private Dictionary<string, Invitation> _invitations;
    private readonly ILogger<InvitationService> _logger;
    private InvitationRepository _invitationRepository;
    public InvitationService(ILogger<InvitationService> logger, InvitationRepository ir)
	{
        _invitations = new Dictionary<string, Invitation>();
        _logger = logger;
        _invitationRepository = ir;
    }

    public async Task<Invitation> AddInvitation(Invitation invitation)
    {
        return await _invitationRepository.AddAsync(invitation);
    }

    public Invitation? GetInvitation(string id)
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

    public Dictionary<string, Invitation> GetAllInvitations()
    {
        return _invitations;
    }

    public Invitation? DeserializeInvitation(JsonElement userJson)
    {
        try
        {
            string eventId = userJson.GetProperty("EventId").GetString();
            string donorId = userJson.GetProperty("DonorId").GetString();
            return new Invitation(eventId, donorId);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {}", e.Message);
            return null;
        }
    }
}

