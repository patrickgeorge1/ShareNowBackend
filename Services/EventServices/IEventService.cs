namespace ShareNowBackend.Services.EventServices;

using System;
using ShareNowBackend.Models;


public interface IEventService
{
    void AddEvent(Event toBeCreatedEvent);
    Event? GetEvent(long id);
}