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

    public async Task<Invitation?> GetInvitation(string id)
    {
        var query = new Invitation()
        {
            Id = id,
        };
        return await _invitationRepository.GetAsync(query);
    }

    public async Task<List<Invitation>> GetAllInvitations()
    {
        return await _invitationRepository.GetAllAsync();
    }
    public async Task<List<Invitation>> GetAllInvitationsFromDonorId(string userId)
    {
        return await _invitationRepository.GetUsGetAllInvitationsFromDonorIderWhereCity(userId);
    }

    public async Task DeleteInvitation(string id)
    {
        var query = new Invitation()
        {
            Id = id
        };
        await _invitationRepository.DeleteAsync(query);
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

