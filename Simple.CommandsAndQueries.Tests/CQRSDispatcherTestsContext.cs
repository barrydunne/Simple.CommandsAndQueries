using Microsoft.Extensions.DependencyInjection;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    internal class CQRSDispatcherTestsContext
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly IServiceProvider _serviceProvider;

        public CQRSDispatcherTestsContext()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection
                .AddCommandAndQueryDispatcher()
                .AddCommandAndQueryHandlers(typeof(MockCommandHandler).Assembly);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            Sut = new(_serviceProvider);
        }

        public CQRSDispatcher Sut { get; }
    }
}
