using CSharpFunctionalExtensions;

namespace Simple.CommandsAndQueries
{
    /// <summary>
    /// The marker for a Command handler with an async handler method.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandAsyncHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Handle the command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <returns>The result of the command operation.</returns>
        Task<Result> HandleAsync(TCommand command);
    }
}
