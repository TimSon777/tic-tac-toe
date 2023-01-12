using MassTransit;

namespace Application;

public sealed class ApplicationMediator : IApplicationMediator
{
    private readonly ICommandBus _commandBus;
    private readonly IEventBus _eventBus;
    private readonly IQueryBus _queryBus;

    public ApplicationMediator(ICommandBus commandBus, IEventBus eventBus, IQueryBus queryBus)
    {
        _commandBus = commandBus;
        _eventBus = eventBus;
        _queryBus = queryBus;
    }

    public async Task<TResult> Command<TCommand, TResult>(TCommand command, CancellationToken ct = new())
        where TCommand : class, ICommand<TResult>
        where TResult : class
    {
        var response = await _commandBus.Request<TCommand, TResult>(command, cancellationToken: ct);
        return response.Message;
    }

    public async Task Event<TEvent>(TEvent @event, CancellationToken ct = new())
        where TEvent : class, IEvent
    {
        await _eventBus.Publish(@event, ct);
    }

    public async Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken ct = new())
        where TQuery : class, IQuery<TResult>
        where TResult : class
    {
        var response = await _queryBus.Request<TQuery, TResult>(query, ct);
        return response.Message;
    }
}