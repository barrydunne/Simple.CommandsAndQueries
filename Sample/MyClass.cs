using Simple.CommandsAndQueries;

namespace Sample
{
    public class MyService
    {
        private readonly ICQRSDispatcher _cqrs;
        public MyService(ICQRSDispatcher cqrs) => _cqrs = cqrs;
        public string GetVersion() => _cqrs.Dispatch(new GetVersionQuery());
        public async Task<MyModel> GetModelAsync() => await _cqrs.DispatchAsync(new GetModelQuery());
    }
}
