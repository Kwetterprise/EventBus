// ---------------------------------------------------------------------------------------------------------------------
//  <copyright file="KafkaEventListener.cs" company="Prodrive B.V.">
//      Copyright (c) Prodrive B.V. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Kwetterprise.EventSourcing.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Subjects;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Kwetterprise.EventSourcing.Models.Event;
    using Microsoft.Extensions.Logging;

    public sealed class KafkaEventListener : IEventListener
    {
        private readonly ILogger<KafkaEventListener> logger;
        private readonly Subject<EventBase> subject = new Subject<EventBase>();
        private readonly IConsumer<Ignore, string> consumer;
        private readonly CancellationTokenSource token;
        private Task? task;

        /// <inheritdoc />
        public KafkaEventListener(ILogger<KafkaEventListener> logger, KafkaConsumerConfiguration configuration, CancellationTokenSource token)
        {
            this.logger = logger;
            var config = new ConsumerConfig
            {
                BootstrapServers = configuration.Servers,
                GroupId = configuration.GroupId,
                AutoOffsetReset = configuration.Offset,
            };
            this.token = token;

            this.consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            this.consumer.Subscribe(configuration.Topics.Select(x => x.ToString()));
        }

        public void Start()
        {
            this.task = Task.Run(this.DoWork, this.token.Token);
        }

        public IDisposable Subscribe(IObserver<EventBase> observer)
        {
            return this.subject.Subscribe(observer);
        }

        public void Dispose()
        {
            this.token.Cancel();

            this.task?.Wait(TimeSpan.FromSeconds(5)); // TODO: Magic number.
            this.task?.Dispose();

            this.consumer.Close();
            this.consumer.Dispose();

            this.subject.Dispose();
        }

        private void DoWork()
        {
            while (!this.token.IsCancellationRequested)
            {
                ConsumeResult<Ignore, string> consumeResult;
                try
                {

                    consumeResult = this.consumer.Consume(this.token.Token);
                }
                catch (ConsumeException e)
                {
                    this.logger.LogError(e, "Unable to consume event(s).");
                    this.subject.OnError(e);
                    return;
                }
                catch (OperationCanceledException)
                {
                    // Cancellation is requested.
                    this.subject.OnCompleted();
                    return;
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
                catch (JsonException e)
                {
                    this.logger.LogError($"Failed to deserialize event: \"{consumeResult.Message.Value}\".");
                    this.subject.OnError(e);
                    return;
                }

                this.subject.OnNext(deserializedEvent);
            }

            this.subject.OnCompleted();
        }
    }
}