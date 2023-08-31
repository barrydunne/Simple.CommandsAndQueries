namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockServiceProvider : IServiceProvider
    {
        private readonly IServiceProvider _provider;
        private readonly HashSet<Type> _requestedTypes;
        private readonly HashSet<Type> _resolvedTypes;

        public MockServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
            _requestedTypes = new();
            _resolvedTypes = new();
        }

        public object? GetService(Type serviceType)
        {
            _requestedTypes.Add(serviceType);
            var result = _provider.GetService(serviceType);
            if (result is not null)
                _resolvedTypes.Add(result.GetType());
            return result;
        }

        public bool Requested<T>() => _requestedTypes.Contains(typeof(T));
        public bool Resolved<T>() => _resolvedTypes.Contains(typeof(T));
    }
}
