using CSharpFunctionalExtensions;

namespace Simple.CommandsAndQueries
{
    /// <summary>
    /// Dispatch commands and queries to the registered handler class.
    /// </summary>
    public interface ICQRSDispatcher
    {
        /// <summary>
        /// Dispatch the command to the registered handler and return the result.
        /// </summary>
        /// <param name="command">The command to dispatch.</param>
        /// <returns>The result from the handler.</returns>
        Result Dispatch(ICommand command);

        /// <summary>
        /// Dispatch the query to the registered handler and return the result.
        /// </summary>
        /// <typeparam name="T">The type of the query result.</typeparam>
        /// <param name="query">The query to dispatch.</param>
        /// <returns>The result from the handler.</returns>
        T Dispatch<T>(IQuery<T> query);

        /// <summary>
        /// Dispatch the command to the registered handler and return the result.
        /// </summary>
        /// <param name="command">The command to dispatch.</param>
        /// <returns>The result from the handler.</returns>
        Task<Result> DispatchAsync(ICommand command);

        /// <summary>
        /// Dispatch the query to the registered handler and return the result.
        /// </summary>
        /// <typeparam name="T">The type of the query result.</typeparam>
        /// <param name="query">The query to dispatch.</param>
        /// <returns>The result from the handler.</returns>
        Task<T> DispatchAsync<T>(IQuery<T> query);
    }
}
