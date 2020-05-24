namespace Kwetterprise.EventSourcing.Client.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Kwetterprise.EventSourcing.Client.Models.Event;

    public interface IEventListener : IObservable<EventBase>, IDisposable
    {
        void StartListening(List<Topic> topics);

        Task Stop();
    }
}