using System;

namespace Kwetterprise.EventSourcing.Client.Models.Event.Tweet
{
    using Kwetterprise.EventSourcing.Client.Models.DataTransfer;

    public sealed class TweetPosted : EventBase
    {
        public TweetPosted()
        {

        }

        public TweetPosted(Guid author, string content, Guid? parentTweet)
            : base(EventType.TweetPosted)
        {
            Author = author;
            Content = content;
            ParentTweet = parentTweet;
        }

        public Guid Author { get; set; }

        public string Content { get; set; } = null!;

        public Guid? ParentTweet { get; set; }
    }
}
