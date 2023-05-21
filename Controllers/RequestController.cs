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

    public RequestController(ILogger<RequestController> logger, RequestService requestService)
    {
        _logger = logger;
        _requestService = requestService;
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
}
