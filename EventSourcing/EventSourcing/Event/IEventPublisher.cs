namespace Kwetterprise.EventSourcing.Client.Event
{
    using System;
    using System.Threading.Tasks;
    using Kwetterprise.EventSourcing.Client.Models.Event;

    public interface IEventPublisher : IDisposable
    {
        Task Publish(EventBase message, Topic topic);
    }
}