namespace Kwetterprise.EventSourcing.Client.Models.Event
{
    public enum EventType
    {
        AccountCreated,
        AccountDeleted,
        AccountUpdated,
        AccountRoleChanged,

        TweetPosted,
        TweetDeleted,
    }
}