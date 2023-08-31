# Simple.CommandsAndQueries

This is a basic library of interfaces to use for CQRS commands and queries.

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
builder.Services
   .AddCommandAndQueryDispatcher()
   .AddCommandAndQueryHandlers(typeof(MyQueryAsyncHandler).Assembly);
```

## Sample usage

```
public class MyClass
{
    private readonly ICQRSDispatcher _cqrs;

    public MyClass(ICQRSDispatcher cqrs) => _cqrs = cqrs;

    public string GetVersion() => _cqrs.Dispatch(new GetVersionQuery());

    public async Task<MyModel> GetModelAsync() => await _cqrs.DispatchAsync(new GetModelQuery());
}

public class GetVersionQuery : IQuery<string> { }

public class GetVersionQueryHandler : IQueryHandler<GetVersionQuery, string>
{
    public string Handle(GetVersionQuery query) => "1.0";
}

public class GetModelQuery : IQuery<MyModel> { }

public class GetModelQueryHandler : IQueryAsyncHandler<GetModelQuery, MyModel>
{
    public async Task<MyModel> HandleAsync(GetModelQuery query) => await ReadMyModelFromDbAsync());
}
```

