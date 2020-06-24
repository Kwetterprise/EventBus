namespace EventSourcing.Client.Test.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Kwetterprise.EventSourcing.Client.Kafka;
    using Kwetterprise.EventSourcing.Client.Models.Event;
    using Kwetterprise.EventSourcing.Client.Models.Event.Account;
    using Kwetterprise.EventSourcing.Client.Models.Event.Tweet;
    using Console = System.Console;

    internal class Program
    {
        private static async Task Main()
        {
            var guid = Guid.NewGuid();
            var topic = new Topic("Test");

            var kafkaConsumerConfiguration = new KafkaConsumerConfiguration("localhost:9092", new List<Topic> { topic }, $"Test", AutoOffsetReset.Earliest, 5000);

            using var publisher = new KafkaEventPublisher(ConsoleLogger<KafkaEventPublisher>.Create(), kafkaConsumerConfiguration);
            using var listener = new KafkaEventListener(ConsoleLogger<KafkaEventListener>.Create(), kafkaConsumerConfiguration);

            static void OnNext(EventBase e)
            {
                switch (e)
                {
                    case TweetDeleted tweetDeleted:
                        Console.WriteLine($"Tweet deleted: {tweetDeleted.Id}. Actor: {tweetDeleted.Actor}.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(e));
                }
            }

            listener.Subscribe(OnNext, error => Console.Write("Error! \n" + error));
            listener.StartListening();

            await publisher.Publish(new TweetDeleted(Guid.NewGuid(), Guid.NewGuid()), topic);

            Console.ReadKey();

            await listener.Stop();
        }
    }
}
