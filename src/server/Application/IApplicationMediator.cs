namespace Application;

public interface IApplicationMediator
{
    Task<TResult> Command<TCommand, TResult>(TCommand command, CancellationToken ct = new())
        where TCommand : class
        where TResult : class;
    
    Task Event<TEvent>(TEvent @event, CancellationToken ct = new())
        where TEvent : class;
    Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken ct = new())
        where TQuery : class
        where TResult : class;
}