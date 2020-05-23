namespace Kwetterprise.EventSourcing.Models.Event.Tweet
{
    using System;

    public sealed class TweetDeletedEvent : EventBase
    {
        public TweetDeletedEvent()
        {

        }

        public TweetDeletedEvent(Guid id)
            : base(EventType.TweetCreatedEvent)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}