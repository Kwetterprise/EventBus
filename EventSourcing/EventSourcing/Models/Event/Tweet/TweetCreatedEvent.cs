namespace Kwetterprise.EventSourcing.Client.Models.Event.Tweet
{
    using Kwetterprise.EventSourcing.Client.Models.DataTransfer;

    public sealed class TweetCreatedEvent : EventBase
    {
        public TweetCreatedEvent()
        {
            
        }

        public TweetCreatedEvent(Tweet tweet)
            : base(EventType.TweetCreatedEvent)
        {
            this.Tweet = tweet;
        }

        public Tweet Tweet { get; set; } = null!;
    }
}
