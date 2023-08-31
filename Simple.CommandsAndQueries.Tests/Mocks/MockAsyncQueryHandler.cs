namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockAsyncQueryHandler : IQueryAsyncHandler<MockAsyncQuery, string>
    {
        public Task<string> HandleAsync(MockAsyncQuery query)
            => query.CausesException ? throw new ApplicationException() : Task.FromResult(query.Payload);
    }
}
