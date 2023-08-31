using CSharpFunctionalExtensions;

namespace Simple.CommandsAndQueries.Tests.Mocks
{
    public class MockAsyncCommandHandler : ICommandAsyncHandler<MockAsyncCommand>
    {
        public Task<Result> HandleAsync(MockAsyncCommand command)
            => command.CausesException ? throw new ApplicationException() : Task.FromResult(Result.Success());
    }
}
