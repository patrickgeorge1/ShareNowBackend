using System;
namespace ShareNowBackend.Models;

public record Request
{
    private static long _idCounter = 0;

    public long Id { get; } = ++_idCounter;
    public long InvitationId { get; init; }
    public long RequesterId { get; init; }
    public RequestStatus Status { get; init; }

    public Request(long invitationId, long requesterId)
    {
        InvitationId = invitationId;
        RequesterId = requesterId;
        Status = RequestStatus.PENDING;
    }
}

