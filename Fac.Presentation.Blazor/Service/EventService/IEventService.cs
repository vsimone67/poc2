using System;
using System.Threading.Tasks;

namespace Fac.Presentation.Blazor.Service.EventService
{
    public interface IEventService
    {
        void Subscribe(string eventName, EventSignature messageHandler);
        void SendEvent(string eventName, EventArgs args);
        Task SendEventAsync(string eventName, EventArgs args);
    }
}
