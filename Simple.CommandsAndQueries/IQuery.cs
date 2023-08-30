namespace Simple.CommandsAndQueries
{
    /// <summary>
    /// The marker for a Query class.
    /// </summary>
    /// <typeparam name="TResult">The result type of the query.</typeparam>
    public interface IQuery<out TResult> { }
}
