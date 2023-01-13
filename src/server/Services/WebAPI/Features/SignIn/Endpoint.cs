using Application.Abstractions;
using Application.Commands.SigIn;
using MinimalApi.Endpoint;

namespace WebAPI.Features.SignIn;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly IApplicationMediator _applicationMediator;

    public Endpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<IResult> HandleAsync(Request request)
    {
        var command = request.MapToCommand();
        var result = await _applicationMediator.Command<SignInCommand, SignInResult>(command);
        var response = result.MapToResponse();
        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("signin", HandleAsync);
    }
}