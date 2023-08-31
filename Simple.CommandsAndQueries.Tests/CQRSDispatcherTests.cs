using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    [TestFixture(Category = "CQRSDispatcher")]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    internal class CQRSDispatcherTests
    {
        private readonly CQRSDispatcherTestsContext _context = new();

        [Test]
        public void CQRSDispatcher_dispatches_command()
        {
            var result = _context.Sut.Dispatch(new MockCommand());
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task CQRSDispatcher_dispatches_async_command()
        {
            var result = await _context.Sut.DispatchAsync(new MockAsyncCommand());
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void CQRSDispatcher_dispatches_query()
        {
            var payload = Guid.NewGuid().ToString();
            var result = _context.Sut.Dispatch(new MockQuery(payload));
            Assert.That(result, Is.EqualTo(payload));
        }

        [Test]
        public async Task CQRSDispatcher_dispatches_async_query()
        {
            var payload = Guid.NewGuid().ToString();
            var result = await _context.Sut.DispatchAsync(new MockAsyncQuery(payload));
            Assert.That(result, Is.EqualTo(payload));
        }
    }
}
