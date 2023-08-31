using Microsoft.Extensions.DependencyInjection;
using Simple.CommandsAndQueries.Tests.Mocks;

namespace Simple.CommandsAndQueries.Tests
{
    internal class ServiceCollectionExtensionsTestsContext
    {
        private readonly IServiceCollection _serviceCollection;

        private IServiceProvider _serviceProvider;
        private MockServiceProvider _serviceProviderWrapper;

        public ServiceCollectionExtensionsTestsContext()
        {
            _serviceCollection = new ServiceCollection();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProviderWrapper = new MockServiceProvider(_serviceProvider);
        }

        public IServiceProvider Provider => _serviceProvider;

        public ICQRSDispatcher Dispatcher => new CQRSDispatcher(_serviceProviderWrapper);

        internal bool ServiceResolved<T>() => _serviceProviderWrapper.Resolved<T>();

        internal ServiceCollectionExtensionsTestsContext WithDispatcher()
        {
            _serviceCollection.AddCommandAndQueryDispatcher();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProviderWrapper = new MockServiceProvider(_serviceProvider);
            return this;
        }

        internal ServiceCollectionExtensionsTestsContext WithDispatcher<T>() where T : class, ICQRSDispatcher
        {
            _serviceCollection.AddCommandAndQueryDispatcher<T>();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProviderWrapper = new MockServiceProvider(_serviceProvider);
            return this;
        }

        internal ServiceCollectionExtensionsTestsContext WithHandlers(bool withLoggingDecorator = false)
        {
            _serviceCollection.AddCommandAndQueryHandlers(typeof(MockCommandHandler).Assembly, withLoggingDecorator);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProviderWrapper = new MockServiceProvider(_serviceProvider);
            return this;
        }
    }
}
