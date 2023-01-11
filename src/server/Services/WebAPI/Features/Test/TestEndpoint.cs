using Application;
using Application.Commands.Test;
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
        var testCommand = request.Map();
        var result = await _applicationMediator.Command<TestCommand, TestCommandResult>(testCommand);
        var response = result.Map();

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("test/{Number}", HandleAsync);
    }
}