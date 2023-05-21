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
public class RequestController : ControllerBase
{
    private readonly ILogger<RequestController> _logger;
    private readonly RequestService _requestService;
    private readonly Service _service;

    public RequestController(ILogger<RequestController> logger, RequestService requestService, Service service)
    {
        _logger = logger;
        _requestService = requestService;
        _service = service;
    }




    // GET: api/Request/5
    [HttpGet("{id}")]
    public IActionResult GetRequestById(long id)
    {
        _logger.LogInformation("GetRequest {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Request? request = _requestService.GetRequest(id);
        if (request != null)
        {
            return Ok(request);
        }

        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAllRequests(long userId)
    {
        _logger.LogInformation("GetAllRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> requests = _requestService.GetAllRequests().Values.ToList();
        return Ok(requests);
    }

    // current user accepted requests
    [HttpGet("accepted/{userId:long}")]
    public IActionResult GetApprovedEventsByUserId(long userId)
    {
        _logger.LogInformation("GetAcceptedRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> acceptedRequests = _requestService.GetAcceptedRequests(userId);
        return Ok(acceptedRequests);
    }

    // current user pending requests
    [HttpGet("pending/{userId:long}")]
    public IActionResult GetPendingEventsByUserId(long userId)
    {
        _logger.LogInformation("GetPendingRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> pendingRequests = _requestService.GetPendingRequests(userId);
        return Ok(pendingRequests);
    }

    // current user requests from other users
    [HttpGet("requested/{userId:long}")]
    public IActionResult GetRequestedEventsByUserId(long userId)
    {
        _logger.LogInformation("GetRequestedRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> requestedRequests = _service.GetRequestedRequests(userId);
        return Ok(requestedRequests);
    }

    // POST: api/Request
    [HttpPost]
    public IActionResult CreateRequest(JsonElement userJson)
    {
        _logger.LogInformation("CreateRequest {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Request? request = _requestService.DeserializeRequest(userJson);
        if (request != null)
        {
            _requestService.AddRequest(request);
            return Ok(request);
        }
        return BadRequest("Invalid input");
    }

    [HttpPost]
    [Route("accept/{id:long}")]
    public IActionResult AcceptRequest(long id)
    {
        _logger.LogInformation("AcceptRequest {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Request? request = _requestService.AcceptRequest(id);
        if (request != null)
        {
            return Ok(request);
        }
        return BadRequest("Invalid input");
    }
}
