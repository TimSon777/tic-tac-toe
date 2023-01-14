using System.Security.Claims;
using Application.Abstractions;
using Application.Queries.CurrentGame;
using MinimalApi.Endpoint;

namespace WebAPI.Features.CurrentGame;

public sealed class Endpoint : IEndpoint<IResult>
{
    private readonly IApplicationMediator _applicationMediator;
    private HttpContext HttpContext { get; set; } = default!;
    
    public Endpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<IResult> HandleAsync()
    {
        var userName = HttpContext.User.UserName();

        var query = new CurrentGameQuery
        {
            UserName = userName
        };

        var result = await _applicationMediator.Query<CurrentGameQuery, CurrentGameQueryResult>(query);

        var response = result.Map();
        
        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("games", async (HttpContext httpContext) =>
        {
            HttpContext = httpContext;
            return await HandleAsync();
        });
    }
}