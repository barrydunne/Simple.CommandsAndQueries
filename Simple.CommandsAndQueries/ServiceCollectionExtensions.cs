using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simple.CommandsAndQueries.Decorators;
using System.Reflection;

namespace Simple.CommandsAndQueries
{
    /// <summary>
    /// Provides extension methods for easy registration of command and query handlers to a service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the default dispatcher service.
        /// </summary>
        /// <param name="services">The service collection to register with.</param>
        public static void AddCommandAndQueryDispatcher(this IServiceCollection services)
            => services.AddTransient<ICQRSDispatcher, CQRSDispatcher>();

        /// <summary>
        /// Add a custom dispatcher service.
        /// </summary>
        /// <typeparam name="T">The type of dispatcher.</typeparam>
        /// <param name="services">The service collection to register with.</param>
        public static void AddCommandAndQueryDispatcher<T>(this IServiceCollection services) where T : class, ICQRSDispatcher
            => services.AddTransient<ICQRSDispatcher, T>();

        /// <summary>
        /// Add all command and query handlers found in the given assembly.
        /// </summary>
        /// <param name="services">The service collection to register with.</param>
        /// <param name="assembly">The assembly containing the handler types.</param>
        /// <param name="withLoggingDecorator">If true a logging decorator will be added to each handler.</param>
        public static void AddCommandAndQueryHandlers(this IServiceCollection services, Assembly assembly, bool? withLoggingDecorator = false)
        {
            RegisterHandlers(services, assembly, typeof(ICommandHandler<>), withLoggingDecorator.GetValueOrDefault() ? typeof(CommandLoggingDecorator<>) : null);
            RegisterHandlers(services, assembly, typeof(ICommandAsyncHandler<>), withLoggingDecorator.GetValueOrDefault() ? typeof(CommandLoggingAsyncDecorator<>) : null);
            RegisterHandlers(services, assembly, typeof(IQueryHandler<,>), withLoggingDecorator.GetValueOrDefault() ? typeof(QueryLoggingDecorator<,>) : null);
            RegisterHandlers(services, assembly, typeof(IQueryAsyncHandler<,>), withLoggingDecorator.GetValueOrDefault() ? typeof(QueryLoggingAsyncDecorator<,>) : null);
        }

        private static void RegisterHandlers(IServiceCollection services, Assembly assembly, Type handlerGenericInterface, Type? loggingDecoratorType)
        {
            var handlerTypes = assembly.GetTypes().Where(_ => !_.IsInterface && !_.IsAbstract && _.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerGenericInterface)).ToList();
            foreach (var handlerType in handlerTypes)
            {
                var handlerInterfaces = handlerType.GetTypeInfo().ImplementedInterfaces;
                var handlerInterface = handlerInterfaces.FirstOrDefault(_ => _.GetGenericTypeDefinition() == handlerGenericInterface);
                if (handlerInterface is not null)
                {
                    if (loggingDecoratorType is null)
                        services.AddTransient(handlerInterface, handlerType);
                    else
                    {
                        services.AddTransient(handlerType);
                        services.AddTransient(handlerInterface, (provider) =>
                        {
                            var handlerGenericTypes = handlerInterface.GetGenericArguments();
                            var closedDecoratorType = loggingDecoratorType.MakeGenericType(handlerGenericTypes);
                            var handlerInstance = provider.GetService(handlerType);
                            var loggerType = typeof(ILogger<>).MakeGenericType(closedDecoratorType);
                            var loggerInstance = provider.GetService(loggerType);
                            var decoratorInstance = Activator.CreateInstance(closedDecoratorType, handlerInstance, loggerInstance);
                            return decoratorInstance!;
                        });
                    }
                }
            }
        }
    }
}
