using Application;
using Application.Commands;
using Application.Commands.Test;
using Application.Queries;
using MassTransit;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddCommands(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<ICommandBus>(typeof(Command<,>), configure);
        return services;
    }

    public static IServiceCollection AddEvents(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<IEventBus>(typeof(Event<>), configure);
        return services;
    }
    
    public static IServiceCollection AddQueries(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<IQueryBus>(typeof(Query<,>), configure);
        return services;
    }
    
    private static void AddConsumers<TBus>(this IServiceCollection services, Type consumerType, Action<IBusRegistrationConfigurator> configure)
        where TBus : class, IBus
    {
        var commandTypes = typeof(AssemblyMarker).Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && t.IsAssignableTo(consumerType))
            .ToArray();

        services.AddMassTransit<TBus>(configurator =>
        {
            configurator.AddConsumers(commandTypes);
            configure.Invoke(configurator);
        });
    }
}