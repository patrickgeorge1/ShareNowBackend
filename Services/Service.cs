using System;
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
}

