using Microsoft.Extensions.DependencyInjection;
using Simple.CommandsAndQueries.Decorators;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    [TestFixture(Category = "ServiceCollectionExtensions")]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class ServiceCollectionExtensionsTests
    {
        private readonly ServiceCollectionExtensionsTestsContext _context = new();

        [Test]
        public void AddCommandAndQueryDispatcher_adds_default_dispatcher()
        {
            _context.WithDispatcher();
            var dispatcher = _context.Provider.GetService<ICQRSDispatcher>();
            Assert.That(dispatcher, Is.Not.Null, "No dispatcher");
            Assert.That(dispatcher, Is.TypeOf<CQRSDispatcher>(), "Wrong dispatcher");
        }

        [Test]
        public void AddCommandAndQueryDispatcher_adds_custom_dispatcher()
        {
            _context.WithDispatcher<MockCQRSDispatcher>();
            var dispatcher = _context.Provider.GetService<ICQRSDispatcher>();
            Assert.That(dispatcher, Is.Not.Null, "No dispatcher");
            Assert.That(dispatcher, Is.TypeOf<MockCQRSDispatcher>(), "Wrong dispatcher");
        }

        [Test]
        public void AddCommandAndQueryHandlers_adds_without_command_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers();

            var result = _context.Dispatcher.Dispatch(new MockCommand());

            Assert.That(_context.ServiceResolved<CommandLoggingDecorator<MockCommand>>(), Is.False, "Decorator used");
            Assert.That(_context.ServiceResolved<MockCommandHandler>(), Is.True, "Handler not used directly");
            Assert.That(result.IsSuccess, Is.True, "Wrong handler result");
        }

        [Test]
        public async Task AddCommandAndQueryHandlers_adds_without_async_command_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers();

            var result = await _context.Dispatcher.DispatchAsync(new MockAsyncCommand());

            Assert.That(_context.ServiceResolved<CommandLoggingDecorator<MockAsyncCommand>>(), Is.False, "Decorator used");
            Assert.That(_context.ServiceResolved<MockAsyncCommandHandler>(), Is.True, "Handler not used directly");
            Assert.That(result.IsSuccess, Is.True, "Wrong handler result");
        }

        [Test]
        public void AddCommandAndQueryHandlers_adds_without_query_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers();

            var payload = Guid.NewGuid().ToString();
            var result = _context.Dispatcher.Dispatch(new MockQuery(payload));

            Assert.That(_context.ServiceResolved<QueryLoggingDecorator<MockQuery, string>>(), Is.False, "Decorator used");
            Assert.That(_context.ServiceResolved<MockQueryHandler>(), Is.True, "Handler not used directly");
            Assert.That(result, Is.EqualTo(payload), "Wrong handler result");
        }

        [Test]
        public async Task AddCommandAndQueryHandlers_adds_without_async_query_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers();

            var payload = Guid.NewGuid().ToString();
            var result = await _context.Dispatcher.DispatchAsync(new MockAsyncQuery(payload));

            Assert.That(_context.ServiceResolved<QueryLoggingAsyncDecorator<MockAsyncQuery, string>>(), Is.False, "Decorator used");
            Assert.That(_context.ServiceResolved<MockAsyncQueryHandler>(), Is.True, "Handler not used directly");
            Assert.That(result, Is.EqualTo(payload), "Wrong handler result");
        }

        [Test]
        public void AddCommandAndQueryHandlers_adds_command_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers(true);

            var result = _context.Dispatcher.Dispatch(new MockCommand());

            Assert.That(_context.ServiceResolved<CommandLoggingDecorator<MockCommand>>(), Is.True, "Decorator not used");
            Assert.That(_context.ServiceResolved<MockCommandHandler>(), Is.False, "Handler used directly");
            Assert.That(result.IsSuccess, Is.True, "Wrong handler result");
        }

        [Test]
        public async Task AddCommandAndQueryHandlers_adds_async_command_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers(true);

            var result = await _context.Dispatcher.DispatchAsync(new MockAsyncCommand());

            Assert.That(_context.ServiceResolved<CommandLoggingAsyncDecorator<MockAsyncCommand>>(), Is.True, "Decorator not used");
            Assert.That(_context.ServiceResolved<MockAsyncCommandHandler>(), Is.False, "Handler used directly");
            Assert.That(result.IsSuccess, Is.True, "Wrong handler result");
        }

        [Test]
        public void AddCommandAndQueryHandlers_adds_query_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers(true);

            var payload = Guid.NewGuid().ToString();
            var result = _context.Dispatcher.Dispatch(new MockQuery(payload));

            Assert.That(_context.ServiceResolved<QueryLoggingDecorator<MockQuery, string>>(), Is.True, "Decorator not used");
            Assert.That(_context.ServiceResolved<MockQueryHandler>(), Is.False, "Handler used directly");
            Assert.That(result, Is.EqualTo(payload), "Wrong handler result");
        }

        [Test]
        public async Task AddCommandAndQueryHandlers_adds_async_query_logging_decorator()
        {
            _context
                .WithDispatcher()
                .WithHandlers(true);

            var payload = Guid.NewGuid().ToString();
            var result = await _context.Dispatcher.DispatchAsync(new MockAsyncQuery(payload));

            Assert.That(_context.ServiceResolved<QueryLoggingAsyncDecorator<MockAsyncQuery, string>>(), Is.True, "Decorator not used");
            Assert.That(_context.ServiceResolved<MockAsyncQueryHandler>(), Is.False, "Handler used directly");
            Assert.That(result, Is.EqualTo(payload), "Wrong handler result");
        }
    }
}
