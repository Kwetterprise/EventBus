namespace Kwetterprise.EventSourcing.Client.Models.Event
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