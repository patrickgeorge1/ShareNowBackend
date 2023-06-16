using System;
using ShareNowBackend.Services;
using ShareNowBackend.Models;

namespace ShareNowBackend.Utils;

public class Helpers
{

	private UserService _userService;
	private EventService _eventService;
    private InvitationService _invitationService;
    private RequestService _requestService;
    private readonly ILogger<Helpers> _logger;

    public Helpers(UserService userService, EventService eventService, InvitationService invitationService, RequestService requestService, ILogger<Helpers> logger)
    {
        _userService = userService;
        _eventService = eventService;
        _invitationService = invitationService;
        _requestService = requestService;
        _logger = logger;
    }

    public async void populateServicesWithMocks()
    {

        var users = (await Task.WhenAll(
            new List<User>()
            {
                new User("Patrick"),
                new User("Alex"),
                new User("Radu"),
                new User("Alin"),
                new User("Iulian"),
                new User("Cristi"),

            }.Select(user => _userService.AddUser(user)).ToArray())).ToList();


        var events = (await Task.WhenAll(
            new List<Event>()
            {
                new Event(
                    authorId: users[1].Id,
                    name: "Ne distram cu Delia",
                    description: "Super concert Delia",
                    location: "Sala Palatului",
                    posterImageUrl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fde.wikipedia.org%2Fwiki%2FDelia_Matache&psig=AOvVaw3asJsu4_g0ysS7Vnlap7_v&ust=1684711007976000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIj4j9uDhf8CFQAAAAAdAAAAABAE"
                ),
                new Event(
                    authorId: users[0].Id,
                    name: "Leon Danaila, sculptorul de creiere",
                    description: "Al treilea cel mai bun neurochirurg din lume este roman si vine sa ne vorbeasca",
                    location: "PR001",
                    posterImageUrl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fde-de.facebook.com%2Fleondanailaoficial%2Fphotos%2F&psig=AOvVaw2VMw8sbR2sW2lKrBVFhu1r&ust=1684711076571000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCNjo4PuDhf8CFQAAAAAdAAAAABAo"
                ),
                new Event(
                    authorId: users[3].Id,
                    name: "Liviu Dragnea, PSD",
                    description: "De la deputat la bucatar",
                    location: "Cantina UPB",
                    posterImageUrl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.tiktok.com%2Fdiscover%2FGateste-cu-liviu-dragnea&psig=AOvVaw2LgchEVI2ZeSl_QfrtNS_c&ust=1684711101416000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCODT0IeEhf8CFQAAAAAdAAAAABAw"
                ),
            }.Select(@event => _eventService.AddEvent(@event)).ToArray())).ToList();


        var invitations = (await Task.WhenAll(
            new List<Invitation>()
            {
                // Alex donates invides to event 0
                new Invitation(
                    eventId: events[0].Id,
                    donatorId: users[1].Id
                ),

                // Patrick donates invides to event 1
                new Invitation(
                    eventId: events[1].Id,
                    donatorId: users[0].Id
                ),
            }.Select(invitation => _invitationService.AddInvitation(invitation)).ToArray())).ToList();


        var requests = (await Task.WhenAll(
            new List<Request>()
            {
                // Radu requests participation from Alex for event 1
                new Request(
                    invitationId: invitations[0].Id,
                    requesterId: users[2].Id
                ),

                // Alin requests participation from Patrick for event 1
                new Request(
                    invitationId: invitations[1].Id,
                    requesterId: users[3].Id
                ),
            }.Select(request => _requestService.AddRequest(request)).ToArray())).ToList();
    }
}

