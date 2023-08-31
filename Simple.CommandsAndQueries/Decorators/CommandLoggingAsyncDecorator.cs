using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Simple.CommandsAndQueries.Decorators
{
    /// <summary>
    /// Applies a logging decorator to an existing handler.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command being decorated.</typeparam>
    public class CommandLoggingAsyncDecorator<TCommand> : ICommandAsyncHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandAsyncHandler<TCommand> _handler;
        private readonly ILogger _logger;
        private readonly string _commandName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoggingAsyncDecorator{TCommand}"/> class.
        /// </summary>
        /// <param name="handler">The handler to decorate.</param>
        /// <param name="logger">The logger to use.</param>
        public CommandLoggingAsyncDecorator(ICommandAsyncHandler<TCommand> handler, ILogger<CommandLoggingAsyncDecorator<TCommand>> logger)
        {
            _handler = handler;
            _logger = logger;
            _commandName = typeof(TCommand).Name;
        }

        /// <inheritdoc/>
        public async Task<Result> HandleAsync(TCommand command)
        {
            _logger?.LogInformation("Handling command {CommandName}", _commandName);
            try
            {
                var result = await _handler.HandleAsync(command);
                _logger?.LogInformation("Handled command {CommandName} with result {Result}", _commandName, result.ToString());
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failure handling command {CommandName}", _commandName);
                throw;
            }
        }
    }
}
