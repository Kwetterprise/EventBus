namespace Kwetterprise.EventSourcing.Event
{
    using System;
    using System.Threading.Tasks;
    using Kwetterprise.EventSourcing.Models.Event;

    public interface IEventPublisher : IDisposable
    {
        Task Publish(EventBase message, Topic topic);
    }
}