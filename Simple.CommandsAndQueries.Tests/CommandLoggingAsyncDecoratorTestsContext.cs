using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Decorators;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    internal class CommandLoggingAsyncDecoratorTestsContext
    {
        private readonly MockLogger<CommandLoggingAsyncDecorator<MockAsyncCommand>> _logger;

        public CommandLoggingAsyncDecoratorTestsContext()
        {
            _logger = new();
            Sut = new CommandLoggingAsyncDecorator<MockAsyncCommand>(new MockAsyncCommandHandler(), _logger);
        }

        public CommandLoggingAsyncDecorator<MockAsyncCommand> Sut { get; }

        internal string GetLoggedMessage(LogLevel logLevel, string content)
        {
            var (category, level, message) = _logger.GetLog().FirstOrDefault(_ => _.Level == logLevel && _.Message == content);
            return message;
        }
    }
}
