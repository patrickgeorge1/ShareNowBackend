using System;
using System.Text.Json;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services;

public class RequestService
{
    private Dictionary<long, Request> _requests;
    private readonly ILogger<UserController> _logger;

    public RequestService(ILogger<UserController> logger)
    {
        _logger = logger;
        _requests = new Dictionary<long, Request>();
    }

    public void AddRequest(Request request)
    {
        _requests.Add(request.Id, request);
    }

    public Request? GetRequest(long id)
    {
        if (_requests.TryGetValue(id, out Request? request))
        {
            return request;
        }
        else
        {
            return null;
        }
    }

    public Request? DeserializeRequest(JsonElement requestJson)
    {
        try
        {
            long invitationId = requestJson.GetProperty("InvitationId").GetInt64();
            long requesterId = requestJson.GetProperty("RequesterId").GetInt64();
            return new Request(invitationId, requesterId);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {}", e.Message);
            return null;
        }
    }
}

