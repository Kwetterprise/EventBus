using System;
using System.Collections.Generic;
using System.Text;
using Kwetterprise.EventSourcing.Client.Models.DataTransfer;

namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    public class AccountCreated : EventBase
    {
        public AccountCreated()
        {
        }

        public AccountCreated(Guid id, string username, string emailAddress, string hashedPassword, DateTime signedUpOn, AccountRole role, string bio, byte[]? profilePicture)
            : base(EventType.AccountCreated)
        {
            this.Id = id;
            this.Username = username;
            this.EmailAddress = emailAddress;
            this.HashedPassword = hashedPassword;
            this.SignedUpOn = signedUpOn;
            Role = role;
            Bio = bio;
            ProfilePicture = profilePicture;
        }

        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public AccountRole Role { get; set; }

        public string Bio { get; set; } = null!;

        public byte[]? ProfilePicture { get; set; }

        public string HashedPassword { get; set; } = null!;

        public DateTime SignedUpOn { get; set; }
    }
}
