using MassTransit;

namespace Application.Commands;

public abstract class CommandHandlerBase<TCommand, TResult> : IConsumer<TCommand>
    where TCommand : class, ICommand<TResult>
{
    protected abstract Task<TResult> Handle(TCommand command);
    
    public async Task Consume(ConsumeContext<TCommand> context)
    {
        var result = await Handle(context.Message);
        await context.RespondAsync(result!);
    }
}