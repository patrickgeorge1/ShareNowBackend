using System;
using System.Text.Json;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services;

public class EventService
{
    private static Dictionary<long, Event> _events;

    private readonly ILogger<EventService> _logger;

    public EventService(ILogger<EventService> logger)
    {
        _logger = logger;
        _events = new Dictionary<long, Event>();
    }

    public void AddEvent(Event toBeCreatedEvent)
    {
        _events.Add(toBeCreatedEvent.Id, toBeCreatedEvent);
    }

    public Event? GetEvent(long id)
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
            long authorId = eventJson.GetProperty("AuthorId").GetInt64();
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

