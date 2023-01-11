using MassTransit;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;

        services.AddEvents(configurator => configurator.UsingRabbitMq(configuration));
        services.AddDbContext(configuration);
    })
    .Build()
    .RunAsync();