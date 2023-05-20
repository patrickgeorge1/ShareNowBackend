using System;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services.EventServices
{
	public class EventService : IEventService
	{
        private static Dictionary<long, Event> _events = new Dictionary<long, Event>();


        public EventService()
		{
		}

        public void AddEvent(Event toBeCreatedEvent)
        {
            _events.Add(toBeCreatedEvent.Id, toBeCreatedEvent);
        }

        public Event? GetEvent(long id)
        {
            if (_events.TryGetValue(id, out Event? @event))
            {
                return @event;
            }
            else
            {
                return null;
            }
        }
    }
}

