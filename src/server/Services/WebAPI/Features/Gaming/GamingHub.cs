using Application.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Features.Gaming;

public sealed class GamingHub : Hub<IGamingClient>
{
    private readonly IApplicationMediator _applicationMediator;

    public GamingHub(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }
}