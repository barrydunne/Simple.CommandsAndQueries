using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    [TestFixture(Category = "QueryLoggingAsyncDecorator")]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    internal class QueryLoggingAsyncDecoratorTests
    {
        private readonly QueryLoggingAsyncDecoratorTestsContext _context = new();

        [Test]
        public async Task QueryLoggingAsyncDecorator_returns_handler_result()
        {
            var payload = Guid.NewGuid().ToString();
            var result = await _context.Sut.HandleAsync(new MockAsyncQuery(payload));
            Assert.That(result, Is.EqualTo(payload));
        }

        [Test]
        public async Task QueryLoggingAsyncDecorator_logs_handling()
        {
            await _context.Sut.HandleAsync(new MockAsyncQuery());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handling query MockAsyncQuery");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public async Task QueryLoggingAsyncDecorator_logs_handled()
        {
            await _context.Sut.HandleAsync(new MockAsyncQuery());
            var message = _context.GetLoggedMessage(LogLevel.Information, "Handled query MockAsyncQuery");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void QueryLoggingAsyncDecorator_logs_error()
        {
            Assert.That(async () => await _context.Sut.HandleAsync(new MockAsyncQuery(causesException: true)), Throws.TypeOf<ApplicationException>());
            var message = _context.GetLoggedMessage(LogLevel.Error, "Failure handling query MockAsyncQuery");
            Assert.That(message, Is.Not.Null.Or.Empty);
        }
    }
}
