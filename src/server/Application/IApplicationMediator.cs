namespace Application;

public interface IApplicationMediator
{
    Task<TResult> Command<TCommand, TResult>(TCommand command, CancellationToken ct = new())
        where TCommand : class, ICommand<TResult>
        where TResult : class;
    
    Task Event<TEvent>(TEvent @event, CancellationToken ct = new())
        where TEvent : class, IEvent;
    
    Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken ct = new())
        where TQuery : class, IQuery<TResult>
        where TResult : class;
}