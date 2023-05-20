using System;
namespace ShareNowBackend.Models;

public record User
{
    private static long _idCounter = 0;

    public long Id { get; } = ++_idCounter;
    public string Name { get; init; }
    public List<long> FriendIds { get; init; }

    public User(string name)
    {
        Name = name;
        FriendIds = new List<long>();
    }
}


