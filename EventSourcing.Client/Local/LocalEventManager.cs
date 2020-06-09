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

        private TaskCompletionSource<bool> tcs;

        public LocalEventManager()
        {
            this.tcs = new TaskCompletionSource<bool>();
            this.tcs.SetResult(false);
        }

        public void StartListening()
        {
            if (!this.tcs.Task.IsCompleted)
            {
                throw new InvalidOperationException("Already listening.");
            }

            this.tcs = new TaskCompletionSource<bool>();
        }

        public Task Stop()
        {
            if (this.tcs.Task.IsCompleted)
            {
                throw new InvalidOperationException("Not listening.");
            }

            this.tcs.SetResult(true);
            return Task.CompletedTask;
        }

        public Task Publish<T>(T message, Topic topic)
            where T : EventBase
        {
            Task.Run(() => this.subject.OnNext(message));

            return Task.CompletedTask;
        }

        public IDisposable Subscribe(IObserver<EventBase> observer)
        {
            return this.subject.Subscribe(observer);
        }

        public void Dispose()
        {
            if (!this.tcs.Task.IsCompleted)
            {
                this.Stop();
            }

            this.subject?.Dispose();
        }
    }
}
