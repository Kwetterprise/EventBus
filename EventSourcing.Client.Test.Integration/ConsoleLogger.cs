namespace EventSourcing.Client.Test.Integration
{
    using System;
    using Microsoft.Extensions.Logging;

    internal class ConsoleLogger<T> : ILogger<T>
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public static ConsoleLogger<T> Create() => new ConsoleLogger<T>();
    }
}