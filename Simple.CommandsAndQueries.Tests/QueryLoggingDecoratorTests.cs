using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    [TestFixture(Category = "QueryLoggingDecorator")]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    internal class QueryLoggingDecoratorTests
    {
        private readonly QueryLoggingDecoratorTestsContext _context = new();

        [Test]
        public void QueryLoggingDecorator_returns_handler_result()
        {
            var payload = Guid.NewGuid().ToString();
            var result = _context.Sut.Handle(new MockQuery(payload));
            Assert.That(result, Is.EqualTo(payload));
        }

        [Test]
        public void QueryLoggingDecorator_logs_handling()
        {
            _context.Sut.Handle(new MockQuery());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handling query MockQuery");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void QueryLoggingDecorator_logs_handled()
        {
            _context.Sut.Handle(new MockQuery());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handled query MockQuery");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void QueryLoggingDecorator_logs_error()
        {
            Assert.That(() => _context.Sut.Handle(new MockQuery(causesException: true)), Throws.TypeOf<ApplicationException>());
            var message = _context.GetLoggedMessage(LogLevel.Error, "Failure handling query MockQuery");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }
    }
}
