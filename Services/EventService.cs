using System;
using System.Text.Json;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;
using ShareNowBackend.Repositories;

namespace ShareNowBackend.Services;

public class EventService
{
    private static Dictionary<string, Event> _events;

    private readonly ILogger<EventService> _logger;
    private readonly EventRepository _eventRepository;

    public EventService(ILogger<EventService> logger, EventRepository er)
    {
        _logger = logger;
        _events = new Dictionary<string, Event>();
        _eventRepository = er;
    }

    public Dictionary<string, Event> GetAllEvents()
    {
        return _events;
    }

    public async Task<Event> AddEvent(Event toBeCreatedEvent)
    {
        return await _eventRepository.AddAsync(toBeCreatedEvent);
    }

    public Event? GetEvent(string id)
    {
        if (_events.TryGetValue(id, out Event? @event))
        {
            return @event;
        }
        else
        {
            return null;
        }
    }

    public Event? DeserializeEvent(JsonElement eventJson)
    {
        try
        {
            string authorId = eventJson.GetProperty("AuthorId").GetString();
            string name = eventJson.GetProperty("Name").GetString() ?? throw new Exception("Could not deserialize json to event.");
            string description = eventJson.GetProperty("Description").GetString() ?? throw new Exception("Could not deserialize json to event.");
            string location = eventJson.GetProperty("Location").GetString() ?? throw new Exception("Could not deserialize json to event.");
            string posterImageURL = eventJson.GetProperty("PosterImageUrl").GetString() ?? throw new Exception("Could not deserialize json to event.");
            return new Event(authorId, name, description, location, posterImageURL);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {}", e.Message);
            return null;
        }
    }
}

