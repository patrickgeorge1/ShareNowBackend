using System;
namespace ShareNowBackend.Models;

public record Event(long Id, long authorId, string Name, string Description, string Location, DateTime Date, string PosterImageUrl);
