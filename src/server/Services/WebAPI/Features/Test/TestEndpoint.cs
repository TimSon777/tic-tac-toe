using Application;
using Application.Commands.Test;
using Application.Queries.Test;
using MinimalApi.Endpoint;

namespace WebAPI.Features.Test;

public sealed class TestEndpoint : IEndpoint<IResult, TestRequest>
{
    private readonly IApplicationMediator _applicationMediator;

    public TestEndpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<IResult> HandleAsync([AsParameters] TestRequest request)
    {
        var testCommand = request.MapToCommand();
        var testQuery = request.MapToQuery();
        
        var commandResult = await _applicationMediator.Command<TestCommand, TestCommandResult>(testCommand);
        var queryResult = await _applicationMediator.Query<TestQuery, TestQueryResult>(testQuery);

        var response = new TestResponse
        {
            NumberCommand = commandResult.Number,
            NumberQuery = queryResult.Number
        };
        
        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("test/{Number}", HandleAsync);
    }
}