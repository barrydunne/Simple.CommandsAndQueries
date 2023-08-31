using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    [TestFixture(Category = "CommandLoggingAsyncDecorator")]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    internal class CommandLoggingAsyncDecoratorTests
    {
        private readonly CommandLoggingAsyncDecoratorTestsContext _context = new();

        [Test]
        public async Task CommandLoggingAsyncDecorator_returns_handler_result()
        {
            var result = await _context.Sut.HandleAsync(new MockAsyncCommand());
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task CommandLoggingAsyncDecorator_logs_handling()
        {
            await _context.Sut.HandleAsync(new MockAsyncCommand());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handling command MockAsyncCommand");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public async Task CommandLoggingAsyncDecorator_logs_handled()
        {
            await _context.Sut.HandleAsync(new MockAsyncCommand());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handled command MockAsyncCommand with result Success");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void CommandLoggingAsyncDecorator_logs_error()
        {
            Assert.That(async () => await _context.Sut.HandleAsync(new MockAsyncCommand(true)), Throws.TypeOf<ApplicationException>());
            var message = _context.GetLoggedMessage(LogLevel.Error, "Failure handling command MockAsyncCommand");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }
    }
}
