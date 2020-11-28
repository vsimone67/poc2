using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fac.Presentation.Blazor.Service.EventService
{
    public delegate Task EventSignature(EventArgs arg);
    public class EventService : IEventService
    {
        private Dictionary<string, EventSignature> EventList;

        public EventService()
        {
            EventList = new Dictionary<string, EventSignature>();
        }

        public void Subscribe(string eventName, EventSignature messageHandler)
        {

            EventList.Add(eventName, messageHandler);
        }

        public void SendEvent(string eventName, EventArgs args)
        {
            if (!EventList.ContainsKey(eventName))
            {
                throw new Exception($"Event {eventName} was not found");
            }
            var eventToSend = EventList.Where(exp => exp.Key == eventName).First();
            eventToSend.Value(args);

        }

        public async Task SendEventAsync(string eventName, EventArgs args)
        {
            if (!EventList.ContainsKey(eventName))
            {
                throw new Exception($"Event {eventName} was not found");
            }

            var eventToSend = EventList.Where(exp => exp.Key == eventName).First();

            Task handlerTask = ((EventSignature)eventToSend.Value)(args);
            await Task.WhenAny(handlerTask);

        }

    }

}
