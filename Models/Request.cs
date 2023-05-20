using System;
namespace ShareNowBackend.Models;

public record Request(long Id, long InvitationId, long RequesterId, RequestStatus Status);

