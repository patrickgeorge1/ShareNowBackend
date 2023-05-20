using System;
namespace ShareNowBackend.Models;

public record Event
{
    private static long _idCounter = 0;

    public long Id { get; } = ++_idCounter;
    public long AuthorId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public DateTime Date { get; init; }
    public string PosterImageUrl { get; init; }

    public Event(long authorId, string name, string description, string location, string posterImageUrl)
    {
        this.AuthorId = authorId;
        Name = name;
        Description = description;
        Location = location;
        PosterImageUrl = posterImageUrl;
        Date = DateTime.Now;
    }
}
