using Application.Abstractions.Repositories;
using Domain.Enums;

namespace Application.Commands.StartGame;

public sealed class StartGameCommandHandler : CommandHandlerBase<StartGameCommand, StartGameCommandResult>
{
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;

    public StartGameCommandHandler(IGameRepository gameRepository, IPlayerRepository playerRepository)
    {
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
    }

    protected override async Task<StartGameCommandResult> Handle(StartGameCommand command)
    {
        var game = await _gameRepository.GetActiveGameByUserNameAsync(command.UserName);
        
        game.Start();

        await _gameRepository.CommitAsync();

        var player = await _playerRepository.GetInitiatorAsync(command.UserName);
        
        return new StartGameCommandResult
        {
            IsStart = true,
            InitiatorUserName = player.User.UserName,
            InitiatorPlayerSign = game.Initiator.PlayerSign.ToStr(),
            PlayerSign = game.Mate!.PlayerSign.ToStr()
        };
    }
}