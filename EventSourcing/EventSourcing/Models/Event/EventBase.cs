namespace Kwetterprise.EventSourcing.Client.Models.Event
{
    using System;
    using Kwetterprise.EventSourcing.Client.Models.Event.Account;

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
            EventType.AccountCreatedEvent => typeof(AccountCreatedEvent),
            EventType.AccountDeletedEvent => typeof(AccountDeletedEvent),
            _ => throw new InvalidOperationException("Unknown event type."),
        };
    }
}