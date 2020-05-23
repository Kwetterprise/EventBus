namespace Kwetterprise.EventSourcing.Client.Models.DataTransfer
{
    using System;

    public sealed class Tweet
    {
        public Tweet()
        {
            
        }

        public Tweet(Guid authorId, string content, Guid parentId)
        {
            this.AuthorId = authorId;
            this.Content = content;
            this.ParentId = parentId;
        }

        public string Content { get; set; } = null!;

        public Guid? ParentId { get; set; }

        public Guid AuthorId { get; set; }
    }
}
