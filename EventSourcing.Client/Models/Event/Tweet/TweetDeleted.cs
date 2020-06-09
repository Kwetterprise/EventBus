namespace Kwetterprise.EventSourcing.Client.Models.Event.Tweet
{
    using System;

    public sealed class TweetDeleted : EventBase
    {
        public TweetDeleted()
        {

        }

        public TweetDeleted(Guid id, Guid actor)
            : base(EventType.TweetDeleted)
        {
            this.Id = id;
            this.Actor = actor;
        }

        public Guid Id { get; set; }

        public Guid Actor { get; set; }
    }
}