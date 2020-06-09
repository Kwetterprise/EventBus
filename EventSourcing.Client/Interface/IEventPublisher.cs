namespace Kwetterprise.EventSourcing.Client.Interface
{
    using System;
    using System.Threading.Tasks;
    using Kwetterprise.EventSourcing.Client.Models.Event;

    public interface IEventPublisher : IDisposable
    {
        Task Publish<T>(T message, Topic topic) where T : EventBase;
    }
}