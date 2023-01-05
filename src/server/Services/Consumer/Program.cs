using Consumer;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;

        services.AddHostedService<Worker>();
        services.AddDbContext(configuration);
    })
    .Build()
    .RunAsync();