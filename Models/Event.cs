using Google.Cloud.Firestore;
using ShareNowBackend.Repositories;
using System;
using System.Text.Json.Serialization;

namespace ShareNowBackend.Models;

[FirestoreData]
public record Event : IBaseFirestoreData
{
    [JsonConstructor]
    public Event() { }

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string AuthorId { get; init; }

    [FirestoreProperty]
    public string Name { get; init; }

    [FirestoreProperty]
    public string Description { get; init; }

    [FirestoreProperty]
    public string Location { get; init; }

    [FirestoreProperty]
    public Timestamp Date { get; init; }

    [FirestoreProperty]
    public string PosterImageUrl { get; init; }

    public Event(string authorId, string name, string description, string location, string posterImageUrl)
    {
        this.AuthorId = authorId;
        Name = name;
        Description = description;
        Location = location;
        PosterImageUrl = posterImageUrl;

        DateTime dateTime = DateTime.Now;
        DateTime utcDateTime = dateTime.ToUniversalTime();
        Date = Timestamp.FromDateTime(utcDateTime);

    }
}
