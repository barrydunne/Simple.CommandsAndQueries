using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Decorators;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    internal class CommandLoggingDecoratorTestsContext
    {
        private readonly MockLogger<CommandLoggingDecorator<MockCommand>> _logger;

        public CommandLoggingDecoratorTestsContext()
        {
            _logger = new();
            Sut = new CommandLoggingDecorator<MockCommand>(new MockCommandHandler(), _logger);
        }

        public CommandLoggingDecorator<MockCommand> Sut { get; }

        internal string GetLoggedMessage(LogLevel logLevel, string content)
        {
            var (category, level, message) = _logger.GetLog().FirstOrDefault(_ => _.Level == logLevel && _.Message == content);
            return message;
        }
    }
}
