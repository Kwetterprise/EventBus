namespace Kwetterprise.EventSourcing.Client.Event
{
    using System;
    using Kwetterprise.EventSourcing.Client.Models.Event;

    public interface IEventListener : IObservable<EventBase>, IDisposable
    {
        void Start();
    }
}