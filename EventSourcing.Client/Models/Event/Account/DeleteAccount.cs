namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using System;

    public sealed class DeleteAccount : EventBase
    {
        public DeleteAccount()
        {

        }

        public DeleteAccount(Guid accountId)
        {
            this.AccountId = accountId;
        }

        public Guid AccountId { get; set; }
    }
}