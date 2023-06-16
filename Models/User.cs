using Google.Cloud.Firestore;
using ShareNowBackend.Repositories;
using System;
using System.Text.Json.Serialization;

namespace ShareNowBackend.Models;

[FirestoreData]
public record User : IBaseFirestoreData
{
    [JsonConstructor]
    public User()
    {
        FriendIds = new List<string>();
    }

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string Name { get; init; }

    [FirestoreProperty]
    public List<string> FriendIds { get; init; }

    public User(string name)
    {
        Name = name;
        FriendIds = new List<string>();
    }
}


