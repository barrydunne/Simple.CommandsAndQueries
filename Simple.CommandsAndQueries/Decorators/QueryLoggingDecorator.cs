using Microsoft.Extensions.Logging;

namespace Simple.CommandsAndQueries.Decorators
{
    /// <summary>
    /// Applies a logging decorator to an existing handler.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query being decorated.</typeparam>
    /// <typeparam name="TResult">The type of the result from the query being decorated.</typeparam>
    public class QueryLoggingDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _handler;
        private readonly ILogger _logger;
        private readonly string _queryName;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryLoggingDecorator{TQuery, TResult}"/> class.
        /// </summary>
        /// <param name="handler">The handler to decorate.</param>
        /// <param name="logger">The logger to use.</param>
        public QueryLoggingDecorator(IQueryHandler<TQuery, TResult> handler, ILogger<QueryLoggingDecorator<TQuery, TResult>> logger)
        {
            _handler = handler;
            _logger = logger;
            _queryName = typeof(TQuery).Name;
        }

        /// <inheritdoc/>
        public TResult Handle(TQuery query)
        {
            _logger.LogInformation("Handling query {QueryName}", _queryName);
            try
            {
                var result = _handler.Handle(query);
                _logger.LogInformation("Handled query {QueryName}", _queryName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure query command {QueryName}", _queryName);
                throw;
            }
        }
    }
}
