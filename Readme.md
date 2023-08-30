# Simple.CommandsAndQueries

This is a basic library if interfaces to use for CQRS commands and queries.

It is inspired by the work of [Vladimir Khorikov](https://github.com/vkhorikov) and his [course repository](https://github.com/vkhorikov/CqrsInPractice)

It provides the basic interfaces

* ICommand
* IQuery

Along with handler interfaces

* ICommandHandler
* ICommandAsyncHandler
* IQueryHandler
* IQueryAsyncHandler

It also includes a dispatcher with interface to be used for processing commands and queries
as well as extensions to the IServiceCollection to register the dispatcher and all commands and queries.


## Registration sample

```
builder.Services.AddCommandAndQueryDispatcher();
builder.Services.AddCommandAndQueryHandlers(typeof(MyQueryAsyncHandler).Assembly);
```

## Sample usage

```
public class MyClass
{
    private readonly ICQRSDispatcher _cqrs;

    public MyClass(ICQRSDispatcher cqrs) => _cqrs = cqrs;

    public async Task<IResult> GetVersionAsync() => await _cqrs.DispatchAsync(new GetVersionQuery());
}
```

