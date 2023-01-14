using System.Security.Claims;
using Application.Abstractions;
using Application.Commands.Move;
using Application.Commands.StartGame;
using Application.Commands.StopGame;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Features.Gaming;

public sealed class GamingHub : Hub<IGamingClient>
{
    private readonly IApplicationMediator _applicationMediator;

    public GamingHub(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public override async Task OnConnectedAsync()
    {
        var userName = Context.User!.UserName();
        
        var command = new StartGameCommand
        {
            UserName = userName
        };

        var result = await _applicationMediator.Command<StartGameCommand, StartGameCommandResult>(command);

        if (result.IsStart)
        {
            await Clients
                .User(result.InitiatorUserName)
                .IsConnected(userName, result.InitiatorPlayerSign);

            await Clients.Caller
                .IsConnected(result.InitiatorUserName, result.PlayerSign);
        }
    }

    public async Task Move(int x, int y)
    {
        var command = new MoveCommand
        {
            X = x,
            Y = y,
            UserName = Context.User!.UserName()
        };

        var result = await _applicationMediator.Command<MoveCommand, MoveCommandResult>(command);

        if (result.IsSuccess)
        {
            await Clients.User(result.MateUserName).MateMoved(x, y, result.GameStatus);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userName = Context.User!.UserName();
        var command = new StopGameCommand
        {
            UserName = userName
        };

        var result = await _applicationMediator.Command<StopGameCommand, StopGameCommandResult>(command);

        if (result.MateUserName is not null)
        {
            await Clients.Clients(result.MateUserName).GameOver(false);
        }
        else
        {
            await Clients.Clients(userName).GameOver(false);
        }
    }
}