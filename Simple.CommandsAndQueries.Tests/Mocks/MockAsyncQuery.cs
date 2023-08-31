namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockAsyncQuery : IQuery<string>
    {
        public MockAsyncQuery(string? payload = null, bool causesException = false)
        {
            Payload = payload ?? Guid.NewGuid().ToString();
            CausesException = causesException;
        }

        public string Payload { get; }
        public bool CausesException { get; }
    }
}
