using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Events.StopGame;
using Domain.Entities;
using Domain.Enums;

namespace Application.Commands.Move;

public sealed class MoveCommandHandler : CommandHandlerBase<MoveCommand, MoveCommandResult>
{
    private readonly IGameRepository _gameRepository;
    private readonly IApplicationMediator _applicationMediator;

    public MoveCommandHandler(IGameRepository gameRepository, IApplicationMediator applicationMediator)
    {
        _gameRepository = gameRepository;
        _applicationMediator = applicationMediator;
    }

    protected override async Task<MoveCommandResult> Handle(MoveCommand command)
    {
        var game = await _gameRepository.GetActiveGameByUserNameAsync(command.UserName);

        if (!Game.IsCorrectCoordinates(command.X, command.Y))
        {
            return new MoveCommandResult
            {
                IsSuccess = false,
                Error = $"Not correct coordinates X: {command.X} Y: {command.Y}",
                GameStatus = game.Status.ToString()
            };
        }
        
        var result = game.Move(command.X, command.Y, command.UserName);
        
        await _gameRepository.UpdateAsync(game);

        var isOk = result == MoveResult.Ok;

        if (!game.IsGameOver)
        {
            return new MoveCommandResult
            {
                IsSuccess = isOk,
                MateUserName = game.GetMateUserName(command.UserName),
                GameStatus = game.Status.ToString()
            };
        }
        
        var @event = new StopGameEvent
        {
            GameId = game.Id,
            GameStatus = game.GetCurrentStatus()
        };

        await _applicationMediator.Event(@event);

        return new MoveCommandResult
        {
            IsSuccess = isOk,
            MateUserName = game.GetMateUserName(command.UserName),
            GameStatus = game.Status.ToString()
        };
    }
}