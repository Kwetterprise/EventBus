using System;

namespace Kwetterprise.EventSourcing.Client.Models.Event.Account
{
    public sealed class AccountUpdated : EventBase
    {
        public AccountUpdated()
        {
            
        }

        public AccountUpdated(Guid id, string newUsername, string newEmailAddress, string newHashedPassword, byte[]? newProfilePicture, string newBio)
            : base(EventType.AccountUpdated)
        {
            this.Id = id;
            this.NewUsername = newUsername;
            this.NewEmailAddress = newEmailAddress;
            this.NewHashedPassword = newHashedPassword;
            this.NewProfilePicture = newProfilePicture;
            this.NewBio = newBio;
        }

        public Guid Id { get; set; }

        public string NewUsername { get; set; } = null!;

        public string NewEmailAddress { get; set; } = null!;

        public string NewHashedPassword { get; set; } = null!;

        public byte[]? NewProfilePicture { get; set; } = null!;

        public string NewBio { get; set; } = null!;
    }
}