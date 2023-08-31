namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockQueryHandler : IQueryHandler<MockQuery, string>
    {
        public string Handle(MockQuery query)
            => query.CausesException ? throw new ApplicationException() : query.Payload;
    }
}
