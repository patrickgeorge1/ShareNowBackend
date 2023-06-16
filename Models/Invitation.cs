using System;
using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using ShareNowBackend.Repositories;
using ShareNowBackend.Utils;

namespace ShareNowBackend.Models;


[FirestoreData]
public record Invitation : IBaseFirestoreData
{
    [JsonConstructor]
    public Invitation() { }

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string EventId { get; init; }

    [FirestoreProperty]
    public string DonatorId { get; init; }

    [FirestoreProperty]
    public string QRcode { get; }

    public Invitation(string eventId, string donatorId)
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

