namespace Kwetterprise.EventSourcing.Models.Event
{
    public enum EventType
    {
        AccountCreatedEvent,
        AccountDeletedEvent,
        AccountUpdatedEvent,

        TweetCreatedEvent,
        TweetDeletedEvent,
    }
}