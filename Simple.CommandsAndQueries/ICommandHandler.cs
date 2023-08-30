using CSharpFunctionalExtensions;

namespace Simple.CommandsAndQueries
{
    /// <summary>
    /// The marker for a Command handler.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Handle the command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <returns>The result of the command operation.</returns>
        Result Handle(TCommand command);
    }
}
