using System;
using System.Text.Json;
using Google.Cloud.Firestore;
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

    public async Task<Request?> GetRequest(string id)
    {
        var query = new Request()
        {
            Id = id
        };
        return await _requestRepository.GetAsync(query);
    }

    public async Task<List<Request>> GetAllRequests()
    {
        return await _requestRepository.GetAllAsync();
    }

    public async Task DeleteRequest(string id)
    {
        var query = new Request()
        {
            Id = id
        };
        await _requestRepository.DeleteAsync(query);
    }


    public async Task<Request?> AcceptRequest(string id)
    {
        var queryGet = new Request()
        {
            Id = id
        };
        var request = await _requestRepository.GetAsync(queryGet);
        if (request != null)
        {
            request.Status = RequestStatus.APPROVED;
            request = await _requestRepository.UpdateAsync(request);
        }

        return request;
    }

    public async Task<List<Request>> GetAcceptedRequests(string userId)
    {
        return await _requestRepository.GetRequestsByStatus(RequestStatus.APPROVED);
    }

    public async Task<List<Request>> GetPendingRequests(string userId)
    {
        return await _requestRepository.GetRequestsByStatus(RequestStatus.PENDING);
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

