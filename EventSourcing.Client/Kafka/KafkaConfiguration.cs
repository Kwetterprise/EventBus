namespace Kwetterprise.EventSourcing.Client.Kafka
{
    using System.Collections.Generic;
    using Confluent.Kafka;
    using Models.Event;

    public class KafkaConfiguration
    {
        public KafkaConfiguration(string servers, int socketTimeoutMs)
        {
            this.Servers = servers;
            this.SocketTimeoutMs = socketTimeoutMs;
        }

        public string Servers { get; }

        public int SocketTimeoutMs { get; }
    }

    public sealed class KafkaConsumerConfiguration : KafkaConfiguration
    {
        public KafkaConsumerConfiguration(
            string servers,
            IEnumerable<Topic> topics,
            string groupId,
            AutoOffsetReset? offset,
            int socketTimeoutMs) : base(servers, socketTimeoutMs)
        {
            this.Topics = topics;
            this.GroupId = groupId;
            this.Offset = offset;
        }

        public IEnumerable<Topic> Topics { get; }

        public string GroupId { get; }

        public AutoOffsetReset? Offset { get; }
    }

}
