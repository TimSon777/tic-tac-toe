using Application.Abstractions;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Features.Gaming;

namespace WebAPI.Implementations;

public sealed class StartGameNotificator : IStartGameNotificator
{
    private readonly IHubContext<GamingHub, IGamingClient> _hub;

    public StartGameNotificator(IHubContext<GamingHub, IGamingClient> hub)
    {
        _hub = hub;
    }

    public async Task NotifyAsync(string initiatorUserName, string userName)
    {
        await _hub.Clients
            .User(initiatorUserName)
            .IsConnected(userName);
    }
}