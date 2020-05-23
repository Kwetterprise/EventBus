namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using Kwetterprise.EventSourcing.Client.Models.DataTransfer;

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