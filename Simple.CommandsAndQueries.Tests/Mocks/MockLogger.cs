using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Simple.CommandsAndQueries.Tests.Mocks
{
    internal class MockLogger<TCategoryName> : ILogger<TCategoryName>
    {
        private readonly string _categoryName;
        private readonly ConcurrentQueue<(string Category, LogLevel Level, string Message)> _log;

        public MockLogger()
        {
            _categoryName = typeof(TCategoryName).Name;
            _log = new();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            => _log.Enqueue((_categoryName, logLevel, formatter(state, exception)));

        public IEnumerable<(string Category, LogLevel Level, string Message)> GetLog() => _log.ToArray();
    }
}
