using Microsoft.Extensions.Logging;

namespace Application.Queries.Test;

public sealed class TestQueryHandler : QueryHandlerBase<TestQuery, TestQueryResult>
{
    private readonly ILogger<TestQueryHandler> _logger;

    public TestQueryHandler(ILogger<TestQueryHandler> logger)
    {
        _logger = logger;
    }

    protected override Task<TestQueryResult> Handle(TestQuery command)
    {
        var number = command.Number + Random.Shared.Next(101, 200);
        
        var result = new TestQueryResult
        {
            Number = number
        };

        _logger.LogInformation("Query Number: {Number}", number);
        
        return Task.FromResult(result);
    }
}