using Microsoft.Extensions.Logging;

namespace Simple.CommandsAndQueries.Decorators
{
    /// <summary>
    /// Applies a logging decorator to an existing handler.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query being decorated.</typeparam>
    /// <typeparam name="TResult">The type of the result from the query being decorated.</typeparam>
    public class QueryLoggingAsyncDecorator<TQuery, TResult> : IQueryAsyncHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryAsyncHandler<TQuery, TResult> _handler;
        private readonly ILogger _logger;
        private readonly string _queryName;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryLoggingAsyncDecorator{TQuery, TResult}"/> class.
        /// </summary>
        /// <param name="handler">The handler to decorate.</param>
        /// <param name="logger">The logger to use.</param>
        public QueryLoggingAsyncDecorator(IQueryAsyncHandler<TQuery, TResult> handler, ILogger<QueryLoggingAsyncDecorator<TQuery, TResult>> logger)
        {
            _handler = handler;
            _logger = logger;
            _queryName = typeof(TQuery).Name;
        }

        /// <inheritdoc/>
        public async Task<TResult> HandleAsync(TQuery query)
        {
            _logger?.LogInformation("Handling query {QueryName}", _queryName);
            try
            {
                var result = await _handler.HandleAsync(query);
                _logger?.LogInformation("Handled query {QueryName}", _queryName);
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failure handling query {QueryName}", _queryName);
                throw;
            }
        }
    }
}
