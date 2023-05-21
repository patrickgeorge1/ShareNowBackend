using System;
using ShareNowBackend.Utils;

namespace ShareNowBackend.Models;

public record Invitation
{
    private static long _idCounter = 0;

    public long Id { get; } = ++_idCounter;
    public long EventId { get; init; }
    public long DonatorId { get; init; }
    public string QRcode { get; }

    public Invitation(long eventId, long donatorId)
    {
        this.EventId = eventId;
        this.DonatorId = donatorId;
        this.QRcode = encodeInvitation(this);
    }

    private string encodeInvitation(Invitation invitation)
    {
        return $"{Constants.InvitationQRPrefix}/{invitation.Id}";
    }
}

