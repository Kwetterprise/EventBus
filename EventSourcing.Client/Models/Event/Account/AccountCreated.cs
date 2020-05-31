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

        public AccountCreated(Guid id, string username, string emailAddress, string salt, string hashedPassword,
            DateTime signedUpOn, byte[]? profilePicture, string bio)
            : base(EventType.AccountCreated)
        {
            Id = id;
            Username = username;
            EmailAddress = emailAddress;
            Salt = salt;
            HashedPassword = hashedPassword;
            SignedUpOn = signedUpOn;
            ProfilePicture = profilePicture;
            Bio = bio;
        }

        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string Salt { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;

        public DateTime SignedUpOn { get; set; }

        public byte[]? ProfilePicture { get; set; } = null!;

        public string Bio { get; set; } = null!;
    }
}
