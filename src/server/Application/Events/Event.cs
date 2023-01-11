using MassTransit;

namespace Application.Events;

public abstract class Event<TEvent> : IConsumer<TEvent>
    where TEvent : class
{
    protected abstract Task Handle(TEvent @event);
    
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        await Handle(context.Message);
    }
}