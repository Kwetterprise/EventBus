namespace Kwetterprise.EventSourcing.Client.Models.Event
{
    public class Topic
    {
        public Topic(string value)
        {
            this.Value = value;
        }

        public static Topic Account = new Topic("Account");
        public static Topic Tweet = new Topic("Tweet");

        public string Value { get; }
    }
}
