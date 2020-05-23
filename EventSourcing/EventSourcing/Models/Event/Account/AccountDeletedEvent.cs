namespace Kwetterprise.EventSourcing.Models.Event.Account
{
    public sealed class AccountDeletedEvent : EventBase
    {
        public AccountDeletedEvent()
        {

        }

        public AccountDeletedEvent(int accountId)
            : base(EventType.AccountDeletedEvent)
        {
            this.AccountId = accountId;
        }

        public int AccountId { get; set; }
    }
}