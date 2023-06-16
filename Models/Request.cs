using Google.Cloud.Firestore;
using ShareNowBackend.Repositories;
using System.Text.Json.Serialization;

namespace ShareNowBackend.Models;

[FirestoreData]
public record Request : IBaseFirestoreData
{
    [JsonConstructor]
    public Request() { }

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string InvitationId { get; init; }

    [FirestoreProperty]
    public string RequesterId { get; init; }

    [FirestoreProperty]
    public RequestStatus Status { get; set; }

    public Request(string invitationId, string requesterId)
    {
        InvitationId = invitationId;
        RequesterId = requesterId;
        Status = RequestStatus.PENDING;
    }


}

