using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Decorators;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    internal class QueryLoggingDecoratorTestsContext
    {
        private readonly MockLogger<QueryLoggingDecorator<MockQuery, string>> _logger;

        public QueryLoggingDecoratorTestsContext()
        {
            _logger = new();
            Sut = new QueryLoggingDecorator<MockQuery, string>(new MockQueryHandler(), _logger);
        }

        public QueryLoggingDecorator<MockQuery, string> Sut { get; }

        internal string GetLoggedMessage(LogLevel logLevel, string content)
        {
            var (category, level, message) = _logger.GetLog().FirstOrDefault(_ => _.Level == logLevel && _.Message == content);
            return message;
        }
    }
}
