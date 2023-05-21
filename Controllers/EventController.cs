using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareNowBackend.Models;
using ShareNowBackend.Services;

namespace ShareNowBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;
    private readonly ILogger<EventController> _logger;

    public EventController(EventService eventService, ILogger<EventController> logger)
    {
        this._eventService = eventService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult GetEventById(long id)
    {
        _logger.LogInformation("GetEvent {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Event? @event = _eventService.GetEvent(id);
        if (@event != null)
        {
            return Ok(@event);
        }

        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAllEvents()
    {
        _logger.LogInformation("GetAllEvents {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Event> events = _eventService.GetAllEvents().Values.ToList();
        return Ok(events);
    }

    [HttpPost]
    public IActionResult CreateEvent(JsonElement eventJson)
    {
        _logger.LogInformation("CreateEvent {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Event? newEvent = _eventService.DeserializeEvent(eventJson);
        if (newEvent != null)
        {
            _eventService.AddEvent(newEvent);
            return Ok(newEvent);
        }
        return BadRequest("Invalid input");
    }
}
