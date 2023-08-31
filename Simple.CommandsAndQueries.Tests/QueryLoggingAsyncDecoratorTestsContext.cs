using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Decorators;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    internal class QueryLoggingAsyncDecoratorTestsContext
    {
        private readonly MockLogger<QueryLoggingAsyncDecorator<MockAsyncQuery, string>> _logger;

        public QueryLoggingAsyncDecoratorTestsContext()
        {
            _logger = new();
            Sut = new QueryLoggingAsyncDecorator<MockAsyncQuery, string>(new MockAsyncQueryHandler(), _logger);
        }

        public QueryLoggingAsyncDecorator<MockAsyncQuery, string> Sut { get; }

        internal string GetLoggedMessage(LogLevel logLevel, string content)
        {
            var (category, level, message) = _logger.GetLog().FirstOrDefault(_ => _.Level == logLevel && _.Message == content);
            return message;
        }
    }
}
