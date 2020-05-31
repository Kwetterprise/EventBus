using System;

namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    using Kwetterprise.EventSourcing.Client.Models.DataTransfer;

    public sealed class CreateAccount : EventBase
    {
        public CreateAccount()
        {

        }

        public CreateAccount(Guid id, string username, string emailAddress, string salt, string hashedPasswords, DateTime signedUpOn, byte[]? profilePicture, string bio)
        {
            Id = id;
            Username = username;
            EmailAddress = emailAddress;
            Salt = salt;
            HashedPasswords = hashedPasswords;
            SignedUpOn = signedUpOn;
            ProfilePicture = profilePicture;
            Bio = bio;
        }

        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string Salt { get; set; } = null!;

        public string HashedPasswords { get; set; }

        public string HashedPassword { get; set; } = null!;

        public DateTime SignedUpOn { get; set; }

        public byte[]? ProfilePicture { get; set; } = null!;

        public string Bio { get; set; } = null!;
    }
}