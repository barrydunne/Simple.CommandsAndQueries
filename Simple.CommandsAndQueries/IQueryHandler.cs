namespace Simple.CommandsAndQueries
{
    /// <summary>
    /// The marker for a Query handler.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result of the query.</typeparam>
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handle the query.
        /// </summary>
        /// <param name="query">The query to handle.</param>
        /// <returns>The result of the query operation.</returns>
        TResult Handle(TQuery query);
    }
}
