namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockAsyncCommand : ICommand
    {
        public MockAsyncCommand(bool causesException = false) => CausesException = causesException;
        public bool CausesException { get; }
    }
}
