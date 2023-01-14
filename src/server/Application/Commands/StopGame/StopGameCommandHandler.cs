using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Events.StopGame;
using Domain.Enums;

namespace Application.Commands.StopGame;

public sealed class StopGameCommandHandler : CommandHandlerBase<StopGameCommand, StopGameCommandResult>
{
    private readonly IGameRepository _gameRepository;
    private readonly IApplicationMediator _applicationMediator;

    public StopGameCommandHandler(IGameRepository gameRepository, IApplicationMediator applicationMediator)
    {
        _gameRepository = gameRepository;
        _applicationMediator = applicationMediator;
    }

    protected override async Task<StopGameCommandResult> Handle(StopGameCommand command)
    {
        var game = await _gameRepository.GetActiveGameByInitiatorUserNameAsync(command.UserName);

        if (game.Mate?.User.UserName is null)
        {
            game.Cancel();
        }
        else
        {
            game.Lose(command.UserName); 
        }

        var @event = new StopGameEvent
        {
            GameId = game.Id,
            GameStatus = game.Status
        };

        await _applicationMediator.Event(@event);

        return new StopGameCommandResult
        {
            GameStatus = game.Status.ToString(),
            MateUserName = game.Initiator.User.UserName == command.UserName
                ? game.Mate?.User.UserName
                : game.Initiator.User.UserName
        };
    }
}