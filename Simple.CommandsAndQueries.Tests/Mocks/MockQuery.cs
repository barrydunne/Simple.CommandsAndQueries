namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockQuery : IQuery<string>
    {
        public MockQuery(string? payload = null, bool causesException = false)
        {
            Payload = payload ?? Guid.NewGuid().ToString();
            CausesException = causesException;
        }

        public string Payload { get; }
        public bool CausesException { get; }
    }
}
