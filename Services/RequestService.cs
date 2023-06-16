using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;
using ShareNowBackend.Repositories;

namespace ShareNowBackend.Services;

public class RequestService
{
    private Dictionary<string, Request> _requests;
    private readonly ILogger<UserController> _logger;
    private RequestRepository _requestRepository;
    public RequestService(ILogger<UserController> logger, RequestRepository requestRepository)
    {
        _logger = logger;
        _requests = new Dictionary<string, Request>();
        _requestRepository = requestRepository;
    }

    public async Task<Request> AddRequest(Request request)
    {
        return await _requestRepository.AddAsync(request);
    }

    public Request? GetRequest(string id)
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

    public Dictionary<string, Request> GetAllRequests()
    {
        return _requests;
    }

    public Request? AcceptRequest(string id)
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

    public List<Request> GetAcceptedRequests(string userId)
    {
        List<Request> result = new();
        IEnumerable<Request> myAcceptedRequests =
            from reqKV in _requests
            where reqKV.Value.RequesterId == userId && reqKV.Value.Status == RequestStatus.APPROVED
            select reqKV.Value;
        result.AddRange(myAcceptedRequests);
        return result;
    }

    public List<Request> GetPendingRequests(string userId)
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
            string invitationId = requestJson.GetProperty("InvitationId").GetString();
            string requesterId = requestJson.GetProperty("RequesterId").GetString();
            return new Request(invitationId, requesterId);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {}", e.Message);
            return null;
        }
    }
}

