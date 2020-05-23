namespace Kwetterprise.EventSourcing.Models.Event.Account
{
    using Kwetterprise.EventSourcing.Models.DataTransfer;

    public sealed class AccountCreatedEvent : EventBase
    {
        public AccountCreatedEvent()
        {

        }

        public AccountCreatedEvent(Account account)
            : base(EventType.AccountCreatedEvent)
        {
            this.Account = account;
        }

        public Account Account { get; set; } = null!;
    }
}