namespace Kwetterprise.EventSourcing.Event
{
    using System.Collections.Generic;
    using Confluent.Kafka;
    using Kwetterprise.EventSourcing.Models.Event;

    public class KafkaConfiguration
    {
        public KafkaConfiguration(string servers)
        {
            this.Servers = servers;
        }

        public string Servers { get; }
    }

    public sealed class KafkaConsumerConfiguration : KafkaConfiguration
    {
        public KafkaConsumerConfiguration(
            string servers,
            IEnumerable<Topic> topics,
            string groupId,
            AutoOffsetReset offset) : base(servers)
        {
            this.Topics = topics;
            this.GroupId = groupId;
            this.Offset = offset;
        }

        public IEnumerable<Topic> Topics { get; }

        public string GroupId { get; }

        public AutoOffsetReset Offset { get; }
    }

}
