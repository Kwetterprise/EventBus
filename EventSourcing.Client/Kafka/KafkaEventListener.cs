namespace Kwetterprise.EventSourcing.Client.Kafka
{
    using System;
    using System.Linq;
    using System.Reactive.Subjects;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Interface;
    using Models.Event;
    using Microsoft.Extensions.Logging;

    public sealed class KafkaEventListener : IEventListener
    {
        private readonly ILogger<KafkaEventListener> logger;
        private readonly KafkaConsumerConfiguration configuration;
        private readonly ConsumerConfig config;
        private readonly Subject<EventBase> subject = new Subject<EventBase>();

        private IConsumer<Ignore, string>? consumer;
        private CancellationTokenSource token = new CancellationTokenSource();
        private Task? task;

        public KafkaEventListener(ILogger<KafkaEventListener> logger, KafkaConsumerConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.config = new ConsumerConfig
            {
                BootstrapServers = configuration.Servers,
                GroupId = configuration.GroupId,
                AutoOffsetReset = configuration.Offset,
            };
        }

        public void StartListening()
        {
            if (this.token != null && !this.token.IsCancellationRequested)
            {
                throw new InvalidOperationException("Already listening.");
            }

            this.token = new CancellationTokenSource();

            this.consumer = new ConsumerBuilder<Ignore, string>(this.config).Build();
            this.consumer.Subscribe(this.configuration.Topics.Select(x => x.ToString()));

            this.task = Task.Run(
                () =>
                {
                    while (!this.token.Token.IsCancellationRequested)
                    {
                        try
                        {
                            this.DoWork();
                        }
                        catch (TaskCanceledException)
                        {
                            // ignore
                        }
                        catch (Exception e)
                        {
                            this.subject.OnError(e);
                            return;
                        }
                    }

                    this.consumer.Close();

                    this.subject.OnCompleted();
                },
                this.token.Token);
        }

        public Task Stop()
        {
            if (this.task == null || this.task.IsCompleted)
            {
                throw new InvalidOperationException("Not listening.");
            }

            this.token.Cancel();
            return this.task!;
        }

        public IDisposable Subscribe(IObserver<EventBase> observer)
        {
            return this.subject.Subscribe(observer);
        }

        public void Dispose()
        {
            this.token.Cancel();
            this.token.Dispose();

            this.task?.Dispose();

            this.consumer?.Close();
            this.consumer?.Dispose();

            this.subject.Dispose();
        }

        private void DoWork()
        {
            ConsumeResult<Ignore, string> consumeResult;
            try
            {

                consumeResult = this.consumer!.Consume(this.token!.Token);
            }
            catch (ConsumeException e)
            {
                this.logger.LogError(e, "Unable to consume event(s).");
                throw;
            }

            EventBase deserializedEvent;
            try
            {
                deserializedEvent =
                    JsonSerializer.Deserialize<EventBase>(
                        consumeResult.Message.Value,
                        new JsonSerializerOptions
                        {
                            Converters = { new KwetterpriseEventConverter(), },
                        });
            }
            catch (JsonException)
            {
                this.logger.LogError($"Failed to deserialize event: \"{consumeResult.Message.Value}\".");
                throw;
            }

            this.subject.OnNext(deserializedEvent);
        }
    }
}