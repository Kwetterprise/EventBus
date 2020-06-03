namespace Kwetterprise.EventSourcing.Client.Interface
{
    using System;
    using System.Threading.Tasks;
    using Models.Event;

    public interface IEventListener : IObservable<EventBase>, IDisposable
    {
        void StartListening();

        Task Stop();
    }
}