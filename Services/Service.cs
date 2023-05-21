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


    public List<Request> GetRequestedRequests(long userId)
    {
        List<Request> result = new();

        // get all invitation where user is donor
        IEnumerable<long> myInvitationsQuery =
            from invitationKV in _invitationService.GetAllInvitations()
            where invitationKV.Value.DonatorId == userId
            select invitationKV.Value.Id;
        HashSet<long> myInvitationsIds = myInvitationsQuery.ToHashSet();

        // get all pending requests for the above invitations
        IEnumerable<Request> myRequestedRequests =
            from reqKV in _requestService.GetAllRequests()
            where
                myInvitationsIds.Contains(reqKV.Value.Id) &&
                reqKV.Value.Status == RequestStatus.PENDING
            select reqKV.Value;
        result.AddRange(myRequestedRequests.ToList());
        return result;
    }
}

