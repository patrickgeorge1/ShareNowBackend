using System;
using ShareNowBackend.Services;
using ShareNowBackend.Models;

namespace ShareNowBackend.Utils;

public class Helpers
{

	private UserService _userService;
	private EventService _eventService;
    private readonly ILogger<Helpers> _logger;

    public Helpers(UserService userService, EventService eventService, ILogger<Helpers> logger)
    {
        _userService = userService;
        _eventService = eventService;
        _logger = logger;
    }

    public void populateServicesWithMocks()
    {

        new List<User>()
        {
            new User("Patrick"),
            new User("Alex"),
            new User("Radu"),
            new User("Alin"),
            new User("Iulian"),
            new User("Cristi"),

        }.ForEach(user => _userService.AddUser(user));

        new List<Event>()
        {
            new Event(
                authorId: 2,
                name: "Ne distram cu Delia",
                description: "Super concert Delia",
                location: "Sala Palatului",
                posterImageUrl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fde.wikipedia.org%2Fwiki%2FDelia_Matache&psig=AOvVaw3asJsu4_g0ysS7Vnlap7_v&ust=1684711007976000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIj4j9uDhf8CFQAAAAAdAAAAABAE"
            ),
            new Event(
                authorId: 3,
                name: "Leon Danaila, sculptorul de creiere",
                description: "Al treilea cel mai bun neurochirurg din lume este roman si vine sa ne vorbeasca",
                location: "PR001",
                posterImageUrl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fde-de.facebook.com%2Fleondanailaoficial%2Fphotos%2F&psig=AOvVaw2VMw8sbR2sW2lKrBVFhu1r&ust=1684711076571000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCNjo4PuDhf8CFQAAAAAdAAAAABAo"
            ),
            new Event(
                authorId: 4,
                name: "Liviu Dragnea, PSD",
                description: "De la deputat la bucatar",
                location: "Cantina UPB",
                posterImageUrl: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.tiktok.com%2Fdiscover%2FGateste-cu-liviu-dragnea&psig=AOvVaw2LgchEVI2ZeSl_QfrtNS_c&ust=1684711101416000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCODT0IeEhf8CFQAAAAAdAAAAABAw"
            ),
        }.ForEach(@event => _eventService.AddEvent(@event));
    }
}

