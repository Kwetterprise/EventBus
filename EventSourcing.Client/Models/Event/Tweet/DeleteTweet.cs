namespace Kwetterprise.EventSourcing.Client.Models.Event.Tweet
{
    using System;

    public sealed class DeleteTweet : EventBase
    {
        public DeleteTweet()
        {

        }

        public DeleteTweet(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}