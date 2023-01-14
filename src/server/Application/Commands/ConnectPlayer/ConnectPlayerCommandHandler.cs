using Application.Abstractions;
using Application.Abstractions.Repositories;

namespace Application.Commands.ConnectPlayer;

public sealed class ConnectPlayerCommandHandler : CommandHandlerBase<ConnectPlayerCommand, ConnectPlayerCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IStartGameNotificator _startGameNotificator;

    public ConnectPlayerCommandHandler(IUserRepository userRepository, IGameRepository gameRepository, IPlayerRepository playerRepository, IStartGameNotificator startGameNotificator)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
        _startGameNotificator = startGameNotificator;
    }

    protected override async Task<ConnectPlayerCommandResult> Handle(ConnectPlayerCommand command)
    {
        if (command.UserName == command.InitiatorUserName)
        {
            return new ConnectPlayerCommandResult
            {
                IsConnect = false
            };
        }
        
        var isActivePlayer = await _userRepository.IsActivePlayerAsync(command.UserName);

        if (isActivePlayer)
        {
            return new ConnectPlayerCommandResult
            {
                IsConnect = false
            };
        }

        var game = await _gameRepository.FindActiveGameByInitiatorUserNameAsync(command.InitiatorUserName);

        if (game is null)
        {
            return new ConnectPlayerCommandResult
            {
                IsConnect = false
            }; 
        }
        
        var player = await _playerRepository.CreatePlayerAsync(command.UserName);

        await _gameRepository.UpdateGameAsync(game, player);

        await _startGameNotificator.NotifyAsync(command.InitiatorUserName, command.UserName);
        
        return new ConnectPlayerCommandResult
        {
            IsConnect = true
        };
    }
}