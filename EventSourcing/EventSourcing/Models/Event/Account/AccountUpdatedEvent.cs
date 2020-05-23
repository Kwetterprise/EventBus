namespace Kwetterprise.EventSourcing.Models.Event.Account
{
    using Kwetterprise.EventSourcing.Models.DataTransfer;

    public sealed class AccountUpdatedEvent : EventBase
    {
        public AccountUpdatedEvent()
        {
            
        }

        public AccountUpdatedEvent(Account newAccount)
            : base(EventType.AccountUpdatedEvent)
        {
            this.NewAccount = newAccount;
        }

        public Account NewAccount { get; set; } = null!;
    }
}