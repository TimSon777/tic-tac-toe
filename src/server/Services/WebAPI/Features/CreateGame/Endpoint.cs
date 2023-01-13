using System.Security.Claims;
using Application.Abstractions;
using Application.Commands.CreateGame;
using MinimalApi.Endpoint;

namespace WebAPI.Features.CreateGame;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private HttpContext HttpContext { get; set; } = default!;
    private readonly IApplicationMediator _applicationMediator;
    
    public Endpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<IResult> HandleAsync([AsParameters] Request request)
    {
        var userName = HttpContext.User.UserName();

        var command = new CreateGameCommand
        {
            UserName = HttpContext.User.UserName()
        };
        
        var result = await _applicationMediator.Command<CreateGameCommand, CreateGameCommandResult>(command);

        var response = result.Map();

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("game", async (HttpContext httpContext, Request request) =>
        {
            HttpContext = httpContext;
            return await HandleAsync(request);
        });
    }
}