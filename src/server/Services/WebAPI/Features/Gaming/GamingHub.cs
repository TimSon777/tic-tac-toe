using System.Security.Claims;
using Application.Abstractions;
using Application.Commands.ConnectPlayer;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Features.Gaming;

public sealed class GamingHub : Hub<IGamingClient>
{
    private readonly IApplicationMediator _applicationMediator;

    public GamingHub(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    // ReSharper disable once UnusedMember.Global
    public async Task ConnectPlayer(string initiatorUserName)
    {
        var userName = Context.User!.UserName();
        var command = new ConnectPlayerCommand
        {
            UserName = userName,
            InitiatorUserName = initiatorUserName
        };

        var result = await _applicationMediator.Command<ConnectPlayerCommand, ConnectPlayerCommandResult>(command);
        
        if (result.IsConnect)
        {
            await Clients
                .User(initiatorUserName)
                .IsConnected(userName);

            await Clients
                .User(userName)
                .IsConnected(result.IsConnect);
        }
        else
        {
            await Clients.Caller
                .IsConnected(result.IsConnect);
        }
    }
}