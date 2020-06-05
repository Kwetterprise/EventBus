using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    public class AccountCreated : EventBase
    {
        public AccountCreated()
        {
            
        }

        public AccountCreated(Guid id, string username, string emailAddress, string hashedPassword,
            DateTime signedUpOn)
            : base(EventType.AccountCreated)
        {
            this.Id = id;
            this.Username = username;
            this.EmailAddress = emailAddress;
            this.HashedPassword = hashedPassword;
            this.SignedUpOn = signedUpOn;
        }

        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;

        public DateTime SignedUpOn { get; set; }
    }
}
