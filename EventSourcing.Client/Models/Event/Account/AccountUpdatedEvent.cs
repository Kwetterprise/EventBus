namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using Kwetterprise.EventSourcing.Client.Models.DataTransfer;

    public sealed class AccountUpdatedEvent : EventBase
    {
        public AccountUpdatedEvent()
        {
            
        }

        public AccountUpdatedEvent(Account updatedAccount)
            : base(EventType.AccountUpdatedEvent)
        {
            this.updatedAccount = updatedAccount;
        }

        public Account updatedAccount { get; set; } = null!;
    }
}