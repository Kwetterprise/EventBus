namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using System;

    public sealed class AccountDeletedEvent : EventBase
    {
        public AccountDeletedEvent()
        {

        }

        public AccountDeletedEvent(Guid accountId)
            : base(EventType.AccountDeletedEvent)
        {
            this.AccountId = accountId;
        }

        public Guid AccountId { get; set; }
    }
}