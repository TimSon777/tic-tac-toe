using MassTransit;

namespace Application.Commands;

public abstract class Command<TCommand, TResult> : IConsumer<TCommand>
    where TCommand : class, IRequest<TResult>
{
    protected abstract Task<TResult> Handle(TCommand command);
    
    public async Task Consume(ConsumeContext<TCommand> context)
    {
        var result = await Handle(context.Message);
        await context.RespondAsync(result!);
    }
}