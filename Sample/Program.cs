using Microsoft.Extensions.DependencyInjection;
using Sample;
using Simple.CommandsAndQueries;

var services = new ServiceCollection();
services
    .AddCommandAndQueryDispatcher()
    .AddCommandAndQueryHandlers(typeof(GetVersionQueryHandler).Assembly)
    .AddTransient<MyService>();

var provider = services.BuildServiceProvider();

var my = provider.GetService<MyService>() ?? throw new ApplicationException("Service resolution failed");

var version = my.GetVersion();
Console.WriteLine(version);

var model = await my.GetModelAsync();
Console.WriteLine(model);
