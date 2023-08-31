namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockCommand : ICommand
    {
        public MockCommand(bool causesException = false) => CausesException = causesException;
        public bool CausesException { get; }
    }
}
