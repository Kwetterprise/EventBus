namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using System;

    public sealed class AccountDeleted : EventBase
    {
        public AccountDeleted()
        {

        }

        public AccountDeleted(Guid accountId, Guid actor)
            : base(EventType.AccountDeleted)
        {
            this.AccountId = accountId;
            this.Actor = actor;
        }

        public Guid AccountId { get; set; }

        public Guid Actor { get; set; }
    }
}