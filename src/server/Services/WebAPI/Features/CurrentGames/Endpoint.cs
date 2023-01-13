using Application.Abstractions;
using Application.Queries.CurrentGames;
using MinimalApi.Endpoint;

namespace WebAPI.Features.CurrentGames;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly IApplicationMediator _applicationMediator;

    public Endpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<IResult> HandleAsync([AsParameters]Request request)
    {
        var query = request.Map();

        var result = await _applicationMediator.Query<CurrentGamesQuery, CurrentGamesQueryResult>(query);
        var response = result.Map();

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("games/current", HandleAsync);
    }
}