namespace Kwetterprise.EventSourcing.Event
{
    using System;
    using Kwetterprise.EventSourcing.Models.Event;

    public interface IEventListener : IObservable<EventBase>, IDisposable
    {
        void Start();
    }
}