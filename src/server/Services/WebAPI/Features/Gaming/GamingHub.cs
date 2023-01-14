using System.Security.Claims;
using Application.Abstractions;
using Application.Commands.Move;
using Application.Commands.StartGame;
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
        var command = new StartGameCommand
        {
            UserName = Context.User!.UserName()
        };

        await _applicationMediator.Command<StartGameCommand, StartGameCommandResult>(command);
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

        await Clients.Caller.UserMoved(result.IsSuccess, result.Error, result.GameStatus);
        
        if (result.IsSuccess)
        {
            await Clients.User(result.MateUserName).MateMoved(x, y, result.GameStatus);
        }
    }
}