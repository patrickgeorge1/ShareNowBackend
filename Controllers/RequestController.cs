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
    public async Task<IActionResult> GetRequestById(string id)
    {
        _logger.LogInformation("GetRequest {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Request? request = await _requestService.GetRequest(id);
        if (request != null)
        {
            return Ok(request);
        }

        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRequests(long userId)
    {
        _logger.LogInformation("GetAllRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> requests = await _requestService.GetAllRequests();
        return Ok(requests);
    }

    // current user accepted requests
    [HttpGet("accepted/{userId}")]
    public async Task<IActionResult> GetApprovedEventsByUserId(string userId)
    {
        _logger.LogInformation("GetAcceptedRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> acceptedRequests = await _requestService.GetAcceptedRequests(userId);
        return Ok(acceptedRequests);
    }

    // current user pending requests
    [HttpGet("pending/{userId}")]
    public async Task<IActionResult> GetPendingEventsByUserId(string userId)
    {
        _logger.LogInformation("GetPendingRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> pendingRequests = await _requestService.GetPendingRequests(userId);
        return Ok(pendingRequests);
    }

    // current user requests from other users
    [HttpGet("requested/{userId}")]
    public async Task<IActionResult> GetRequestedEventsByUserId(string userId)
    {
        _logger.LogInformation("GetRequestedRequests {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        List<Request> requestedRequests = await _service.GetRequestedRequests(userId);
        return Ok(requestedRequests);
    }

    // POST: api/Request
    [HttpPost]
    public async Task<IActionResult> CreateRequest(JsonElement userJson)
    {
        _logger.LogInformation("CreateRequest {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Request? request = _requestService.DeserializeRequest(userJson);
        if (request != null)
        {
            return Ok(await _requestService.AddRequest(request));
        }
        return BadRequest("Invalid input");
    }

    [HttpPost]
    [Route("accept/{id}")]
    public async Task<IActionResult> AcceptRequest(string id)
    {
        _logger.LogInformation("AcceptRequest {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);
        Request? request = await _requestService.AcceptRequest(id);
        if (request != null)
        {
            return Ok(request);
        }
        return BadRequest("Invalid input");
    }
}
