using System;
namespace ShareNowBackend.Models;

public record Invitation(long Id, long EventId, long donatorId, string QRcode);

