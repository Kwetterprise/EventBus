
namespace EventSourcing.Test
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Threading;
    using System.Threading.Tasks;
    using Kwetterprise.EventSourcing.Client.Local;
    using Kwetterprise.EventSourcing.Client.Models.Event;
    using Kwetterprise.EventSourcing.Client.Models.Event.Account;
    using Xunit;

    public class LocalEventManagerTest
    {
        [Fact]
        public async Task EventReceived()
        {
            var local = new LocalEventManager();

            var manualResetEvent = new ManualResetEvent(false);

            local.Subscribe(_ => { });
            local.Subscribe(_ => manualResetEvent.Set());
            local.Subscribe(_ => { });
            local.StartListening();

            await local.Publish(
                new AccountCreated(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty,
                    DateTime.UtcNow), Topic.Account);

            manualResetEvent.WaitOne(TimeSpan.FromSeconds(10));
        }

        [Fact]
        public async Task BufferTest()
        {
            var local = new LocalEventManager();

            local.StartListening();
            var task = local.Take(1).Timeout(TimeSpan.FromSeconds(10)).ToTask();

            await local.Publish(new AccountCreated(Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, DateTime.UtcNow), Topic.Account);
            await local.Stop();

            await task;
        }
    }
}
