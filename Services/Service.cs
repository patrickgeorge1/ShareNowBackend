using System;
using ShareNowBackend.Models;
using ShareNowBackend.Services;

namespace ShareNowBackend.Services;


public class Service
{
	private readonly UserService _userService;
	private readonly EventService _eventsService;
	private readonly InvitationService _invitationService;
	private readonly RequestService _requestService;

    public Service(UserService userService, EventService eventsService, InvitationService invitationService, RequestService requestService)
	{
        _userService = userService;
        _eventsService = eventsService;
        _invitationService = invitationService;
        _requestService = requestService;
    }


    public async Task<List<Request>> GetRequestedRequests(string userId)
    {
        List<Request> result = new();

        // get all ids of invitations owned by userId
        HashSet<string> invitationIdsOfUser = 
            (await _invitationService.GetAllInvitationsFromDonorId(userId))
            .Select(i => i.Id)
            .ToHashSet();

        // get all pending requests for the above invitations
        IEnumerable<Request> myRequestedRequests =
            from reqKV in await _requestService.GetAllRequests()
            where
                invitationIdsOfUser.Contains(reqKV.Id) &&
                reqKV.Status == RequestStatus.PENDING
            select reqKV;
        result.AddRange(myRequestedRequests.ToList());
        return result;
    }
}

