using CSharpFunctionalExtensions;

namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockCommandHandler : ICommandHandler<MockCommand>
    {
        public Result Handle(MockCommand command)
            => command.CausesException ? throw new ApplicationException() : Result.Success();
    }
}
