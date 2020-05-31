namespace Kwetterprise.EventSourcing.Client.Models.Event.Tweet
{
    using System;

    public sealed class TweetDeleted : EventBase
    {
        public TweetDeleted()
        {

        }

        public TweetDeleted(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}