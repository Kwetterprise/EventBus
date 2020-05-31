namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using System;

    public sealed class AccountDeleted : EventBase
    {
        public AccountDeleted()
        {

        }

        public AccountDeleted(Guid accountId)
            : base(EventType.AccountDeleted)
        {
            this.AccountId = accountId;
        }

        public Guid AccountId { get; set; }
    }
}