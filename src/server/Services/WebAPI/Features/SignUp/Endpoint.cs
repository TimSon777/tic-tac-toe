using Application.Abstractions;
using Application.Commands.SignUp;
using MinimalApi.Endpoint;

namespace WebAPI.Features.SignUp;

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
        var result = await _applicationMediator.Command<SignUpCommand, SignUpResult>(command);
        var response = result.MapToResponse();
        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("signup", HandleAsync)
            .AllowAnonymous();
    }
}