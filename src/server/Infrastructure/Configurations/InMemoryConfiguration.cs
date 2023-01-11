// ReSharper disable once CheckNamespace
namespace MassTransit;

public static class InMemoryConfiguration
{
    public static IBusRegistrationConfigurator UsingInMemory(this IBusRegistrationConfigurator configurator)
    {
        configurator.UsingInMemory((ctx, inMemoryConfigurator) =>
        {
            inMemoryConfigurator.ConfigureEndpoints(ctx);
        });

        return configurator;
    }
}