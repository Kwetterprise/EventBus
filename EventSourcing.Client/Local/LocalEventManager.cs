using System;
using System.Collections.Generic;

namespace Kwetterprise.EventSourcing.Client.Local
{
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Kwetterprise.EventSourcing.Client.Interface;
    using Kwetterprise.EventSourcing.Client.Models.Event;

    public class LocalEventManager : IEventPublisher, IEventListener
    {
        private readonly Subject<EventBase> subject = new Subject<EventBase>();

        private TaskCompletionSource<bool>? tcs;
        private List<Topic> topics = new List<Topic>();

        public LocalEventManager()
        {
            this.tcs = new TaskCompletionSource<bool>();
            this.tcs.SetResult(false);
        }

        public Task StartListening(List<Topic> topics)
        {
            if (this.tcs != null && !this.tcs.Task.IsCompleted)
            {
                throw new InvalidOperationException("Already listening.");
            }

            this.topics = topics;

            this.tcs = new TaskCompletionSource<bool>();

            return this.tcs.Task;
        }

        public void Stop()
        {
            if (this.tcs == null || this.tcs!.Task.IsCompleted)
            {
                throw new InvalidOperationException("Not listening.");
            }

            this.tcs.SetResult(true);
            this.tcs = null;
        }

        public Task Publish(EventBase message, Topic topic)
        {
            if (this.tcs != null && this.topics.Contains(topic))
            {
                Task.Run(() => this.subject.Publish(message));
            }

            return Task.CompletedTask;
        }

        public IDisposable Subscribe(IObserver<EventBase> observer)
        {
            return this.subject.Subscribe(observer);
        }

        public void Dispose()
        {
            if (this.tcs != null && !this.tcs!.Task.IsCompleted)
            {
                this.Stop();
            }

            this.subject?.Dispose();
        }
    }
}
