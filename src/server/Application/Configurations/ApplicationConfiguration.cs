using Application;
using Application.Commands;
using Application.Events;
using Application.Queries;
using MassTransit;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddCommands(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<ICommandBus>(typeof(CommandHandlerBase<,>), typeof(ICommand<>), configure);
        return services;
    }

    public static IServiceCollection AddEvents(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<IEventBus>(typeof(EventHandlerBase<>), typeof(IEvent), configure);
        return services;
    }
    
    public static IServiceCollection AddQueries(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<IQueryBus>(typeof(QueryHandlerBase<,>), typeof(IQuery<>), configure);
        return services;
    }
    
    private static void AddConsumers<TBus>(this IServiceCollection services,
        Type consumerType,
        Type requestType,
        Action<IBusRegistrationConfigurator> configure)
        where TBus : class, IBus
    {
        var assembly = typeof(AssemblyMarker).Assembly;
        
        var consumers = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && t.IsAssignableToGeneric(consumerType))
            .ToArray();

        var requests = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && (t.IsAssignableTo(requestType) || t.IsAssignableToGeneric(requestType)))
            .ToArray();
        
        services.AddMassTransit<TBus>(configurator =>
        {
            configurator.AddConsumers(consumers);
            configure.Invoke(configurator);

            foreach (var request in requests)
            {
                configurator.AddRequestClient(request);
            }
        });
    }
}