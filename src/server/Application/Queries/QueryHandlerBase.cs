using MassTransit;

namespace Application.Queries;

public abstract class QueryHandlerBase<TQuery, TResult> : IConsumer<TQuery>
    where TQuery : class, IQuery<TResult>
{
    protected abstract Task<TResult> Handle(TQuery command);
    
    public async Task Consume(ConsumeContext<TQuery> context)
    {
        var result = await Handle(context.Message);
        await context.RespondAsync(result!);
    }
}