using System;
using ShareNowBackend.Services.EventServices;
using ShareNowBackend.Services.InvitationServices;
using ShareNowBackend.Services.RequestServices;
using ShareNowBackend.Services.UserServices;

namespace ShareNowBackend.Services;


public class Service : IService
{
	private readonly UserService _userService;
	private readonly IEventService _eventsService;
	private readonly IInvitationService _invitationService;
	private readonly IRequestService _requestService;

    public Service(UserService userService, IEventService eventsService, IInvitationService invitationService, IRequestService requestService)
	{
        _userService = userService;
        _eventsService = eventsService;
        _invitationService = invitationService;
        _requestService = requestService;
    }
}

