namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using Kwetterprise.EventSourcing.Client.Models.DataTransfer;

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