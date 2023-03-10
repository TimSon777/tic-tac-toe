using Application.Abstractions;
using Application.Queries.Rating;
using MinimalApi.Endpoint;

namespace WebAPI.Features.Rating;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly IApplicationMediator _applicationMediator;

    public Endpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<IResult> HandleAsync([AsParameters] Request request)
    {
        var query = new RatingQuery
        {
            ItemsNumber = request.ItemsNumber
        };

        var result = await _applicationMediator.Query<RatingQuery, RatingQueryResult>(query);
        
        var response = result.Map();
        
        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("rating", HandleAsync);
    }
}