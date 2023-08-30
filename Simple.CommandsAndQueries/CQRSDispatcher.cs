using CSharpFunctionalExtensions;

namespace Simple.CommandsAndQueries
{
    /// <inheritdoc/>
    public class CQRSDispatcher : ICQRSDispatcher
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CQRSDispatcher"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="System.IServiceProvider"/> provider used to obtain the handler classes.</param>
        public CQRSDispatcher(IServiceProvider provider) => _provider = provider;

        /// <inheritdoc/>
        public Result Dispatch(ICommand command)
        {
            var type = typeof(ICommandHandler<>);
            var typeArgs = new[] { command.GetType() };
            var handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _provider.GetService(handlerType) ?? throw new ApplicationException($"No handler for {command?.GetType()?.Name}");
            return handler.Handle((dynamic)command);
        }

        /// <inheritdoc/>
        public T Dispatch<T>(IQuery<T> query)
        {
            var type = typeof(IQueryHandler<,>);
            var typeArgs = new[] { query.GetType(), typeof(T) };
            var handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _provider.GetService(handlerType) ?? throw new ApplicationException($"No handler for {query?.GetType()?.Name}");
            return handler.Handle((dynamic)query);
        }

        /// <inheritdoc/>
        public async Task<Result> DispatchAsync(ICommand command)
        {
            var type = typeof(ICommandAsyncHandler<>);
            var typeArgs = new[] { command.GetType() };
            var handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _provider.GetService(handlerType) ?? throw new ApplicationException($"No async handler for {command?.GetType()?.Name}");
            return await handler.HandleAsync((dynamic)command);
        }

        /// <inheritdoc/>
        public async Task<T> DispatchAsync<T>(IQuery<T> query)
        {
            var type = typeof(IQueryAsyncHandler<,>);
            var typeArgs = new[] { query.GetType(), typeof(T) };
            var handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _provider.GetService(handlerType) ?? throw new ApplicationException($"No async handler for {query?.GetType()?.Name}");
            return await handler.HandleAsync((dynamic)query);
        }
    }
}
