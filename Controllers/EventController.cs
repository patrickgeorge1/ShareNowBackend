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
    public async Task<IActionResult> GetEventById(string id)
    {
        _logger.LogInformation("GetEvent {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Event? @event = await _eventService.GetEvent(id);
        if (@event != null)
        {
            return Ok(@event);
        }

        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        _logger.LogInformation("GetAllEvents {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Event> events = await _eventService.GetAllEvents();
        return Ok(events);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(JsonElement eventJson)
    {
        _logger.LogInformation("CreateEvent {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Event? newEvent = _eventService.DeserializeEvent(eventJson);
        if (newEvent != null)
        {
            return Ok(await _eventService.AddEvent(newEvent));
        }
        return BadRequest("Invalid input");
    }
}
