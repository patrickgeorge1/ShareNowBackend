using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
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

    public Dictionary<long, Request> GetAllRequests()
    {
        return _requests;
    }

    public Request? AcceptRequest(long id)
    {
        if (_requests.TryGetValue(id, out Request? request))
        {
            request.Status = RequestStatus.APPROVED;
            _requests[request.Id] = request;
            return request;
        }
        else
        {
            return null;
        }
    }

    public List<Request> GetAcceptedRequests(long userId)
    {
        List<Request> result = new();
        IEnumerable<Request> myAcceptedRequests =
            from reqKV in _requests
            where reqKV.Value.RequesterId == userId && reqKV.Value.Status == RequestStatus.APPROVED
            select reqKV.Value;
        result.AddRange(myAcceptedRequests);
        return result;
    }

    public List<Request> GetPendingRequests(long userId)
    {
        List<Request> result = new();
        IEnumerable<Request> myPendingRequests =
            from reqKV in _requests
            where reqKV.Value.RequesterId == userId && reqKV.Value.Status == RequestStatus.PENDING
            select reqKV.Value;
        result.AddRange(myPendingRequests);
        return result;
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

