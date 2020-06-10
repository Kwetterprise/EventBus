namespace Kwetterprise.EventSourcing.Client.Kafka
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;
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
        private readonly ReplaySubject<EventBase> subject = new ReplaySubject<EventBase>();
        private readonly IConsumer<Ignore, string> consumer;

        private CancellationTokenSource token = new CancellationTokenSource(TimeSpan.Zero);
        private Task? task;

        public KafkaEventListener(ILogger<KafkaEventListener> logger, KafkaConsumerConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            var config = new ConsumerConfig
            {
                BootstrapServers = configuration.Servers,
                GroupId = configuration.GroupId,
                EnableAutoOffsetStore = configuration.Offset.HasValue,
                AutoOffsetReset = configuration.Offset,
            };

            this.consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public void StartListening()
        {
            if (this.token != null && !this.token.IsCancellationRequested)
            {
                throw new InvalidOperationException("Already listening.");
            }

            this.token = new CancellationTokenSource();

            this.task = Task.Run(
                () =>
                {
                    foreach (var eventBase in KafkaEventListener.DoWork(this.logger, this.consumer, this.token.Token))
                    {
                        this.subject.OnNext(eventBase);
                    }
                },
                this.token.Token)
                .ContinueWith(
                    prevTask =>
                    {
                        this.consumer.Unsubscribe();

                        if (prevTask.IsFaulted)
                        {
                            this.subject.OnError(prevTask.Exception);
                        }
                        else
                        {
                            this.subject.OnCompleted();
                        }

                        this.task = null;
                    });

            this.consumer.Subscribe(this.configuration.Topics.Select(x => x.Value));
        }

        public Task Stop()
        {
            if (this.task == null || this.task.IsCompleted)
            {
                throw new InvalidOperationException("Not listening.");
            }

            this.token.Cancel();
            return this.task;
        }

        public IDisposable Subscribe(IObserver<EventBase> observer)
        {
            return this.subject.Subscribe(observer);
        }

        public void Dispose()
        {
            this.token.Cancel();
            this.token.Dispose();

            this.task?.Wait(1000);

            this.consumer.Close();
            this.consumer.Dispose();

            this.subject.Dispose();
        }

        private static IEnumerable<EventBase> DoWork(
            ILogger logger,
            IConsumer<Ignore, string> consumer,
            CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                ConsumeResult<Ignore, string> consumeResult;
                try
                {
                    consumeResult = consumer.Consume(token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                    yield break;
                }
                catch (ConsumeException e)
                {
                    logger.LogError(e, "Unable to consume event(s).");
                    throw;
                }

                EventBase @event;

                try
                {
                    @event = KafkaEventListener.TranslateJson(consumeResult.Message.Value);
                }
                catch (JsonException)
                {
                    logger.LogError($"Failed to deserialize event: \"{consumeResult.Message.Value}\".");
                    throw;
                }

                yield return @event;
            }
        }

        private static EventBase TranslateJson(string json)
        {
            var jsonDocument = JsonDocument.Parse(json);
            var type = EventBase.EventTypeEnumToType(
                (EventType)jsonDocument.RootElement
                    .EnumerateObject()
                    .First(x => x.NameEquals(nameof(EventBase.Type)))
                    .Value.GetInt32());

            return (EventBase)JsonSerializer.Deserialize(json, type);
        }
    }
}