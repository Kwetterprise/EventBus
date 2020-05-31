using System;
using Kwetterprise.EventSourcing.Client.Models.Event.Account;
using Kwetterprise.EventSourcing.Client.Models.Event.Tweet;

namespace Kwetterprise.EventSourcing.Client.Models.Event
{
    public abstract class EventBase
    {
        public EventBase()
        {

        }

        protected EventBase(EventType type)
        {
            this.Type = type;
        }

        public EventType Type { get; set; }

        public static Type EventTypeEnumToType(EventType type) => type switch
        {
            EventType.AccountCreated => typeof(AccountCreated),
            EventType.AccountDeleted => typeof(AccountDeleted),
            EventType.AccountUpdated => typeof(AccountUpdated),
            EventType.AccountRoleChanged => typeof(AccountRoleChanged),
            EventType.TweetPosted => typeof(TweetPosted),
            EventType.TweetDeleted => typeof(TweetDeleted),
            _ => throw new InvalidOperationException("Unknown event type."),
        };
    }
}