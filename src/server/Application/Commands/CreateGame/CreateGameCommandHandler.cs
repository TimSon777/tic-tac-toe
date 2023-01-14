using Application.Abstractions.Repositories;

namespace Application.Commands.CreateGame;

public sealed class CreateGameCommandHandler : CommandHandlerBase<CreateGameCommand, CreateGameCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;

    public CreateGameCommandHandler(IUserRepository userRepository, IGameRepository gameRepository, IPlayerRepository playerRepository)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
    }

    protected override async Task<CreateGameCommandResult> Handle(CreateGameCommand command)
    {
        var isActivePlayer = await _userRepository.IsActivePlayerAsync(command.UserName);

        if (isActivePlayer)
        {
            return new CreateGameCommandResult
            {
                IsCreated = false
            };
        }

        var initiator = await _playerRepository.CreatePlayerAsync(command.UserName);
        await _gameRepository.CreateGameAsync(initiator);

        return new CreateGameCommandResult
        {
            IsCreated = true
        };
    }
}