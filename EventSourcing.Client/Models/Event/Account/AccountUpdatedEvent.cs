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
            this.UpdatedAccount = updatedAccount;
        }

        public Account UpdatedAccount { get; set; } = null!;
    }
}