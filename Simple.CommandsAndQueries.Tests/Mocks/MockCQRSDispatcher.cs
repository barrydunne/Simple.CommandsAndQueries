using CSharpFunctionalExtensions;

namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockCQRSDispatcher : ICQRSDispatcher
    {
        public Result Dispatch(ICommand command) => throw new NotImplementedException();
        public T Dispatch<T>(IQuery<T> query) => throw new NotImplementedException();
        public Task<Result> DispatchAsync(ICommand command) => throw new NotImplementedException();
        public Task<T> DispatchAsync<T>(IQuery<T> query) => throw new NotImplementedException();
    }
}
