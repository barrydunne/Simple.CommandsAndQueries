using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    [TestFixture(Category = "CommandLoggingDecorator")]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    internal class CommandLoggingDecoratorTests
    {
        private readonly CommandLoggingDecoratorTestsContext _context = new();

        [Test]
        public void CommandLoggingDecorator_returns_handler_result()
        {
            var result = _context.Sut.Handle(new MockCommand());
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void CommandLoggingDecorator_logs_handling()
        {
            _context.Sut.Handle(new MockCommand());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handling command MockCommand");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void CommandLoggingDecorator_logs_handled()
        {
            _context.Sut.Handle(new MockCommand());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handled command MockCommand with result Success");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void CommandLoggingDecorator_logs_error()
        {
            Assert.That(() => _context.Sut.Handle(new MockCommand(true)), Throws.TypeOf<ApplicationException>());
            var message = _context.GetLoggedMessage(LogLevel.Error, "Failure handling command MockCommand");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }
    }
}
