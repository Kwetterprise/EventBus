namespace Kwetterprise.EventSourcing.Client.Models.DataTransfer
{
    using System;

    public sealed class Account
    {
        public Account()
        {
            
        }

        public Account(
            Guid id,
            string userName,
            string emailAddress,
            string salt,
            string hashedPassword,
            AccountRole accountRole,
            DateTime signedUpOn,
            byte[]? profilePicture,
            string bio)
        {
            this.Id = id;
            this.Username = userName;
            this.EmailAddress = emailAddress;
            Salt = salt;
            this.HashedPassword = hashedPassword;
            this.AccountRole = accountRole;
            this.SignedUpOn = signedUpOn;
            this.ProfilePicture = profilePicture;
            this.Bio = bio;
        }

        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string Salt { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;

        public AccountRole AccountRole { get; set; }

        public DateTime SignedUpOn { get; set; }

        public byte[]? ProfilePicture { get; set; } = null!;

        public string Bio { get; set; } = null!;
    }
}