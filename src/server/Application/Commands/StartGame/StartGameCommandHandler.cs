using Application.Abstractions.Repositories;

namespace Application.Commands.StartGame;

public sealed class StartGameCommandHandler : CommandHandlerBase<StartGameCommand, StartGameCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;

    public StartGameCommandHandler(IUserRepository userRepository, IGameRepository gameRepository)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    protected override async Task<StartGameCommandResult> Handle(StartGameCommand command)
    {
        var user = await _userRepository.FindByUserNameWithReadyPlayerAsync(command.UserName);

        if (user is null || !user.IsReadyToStartPlayer())
        {
            return new StartGameCommandResult
            {
                IsStart = false
            };
        }

        var game = user.CurrentGame;
        
        game.Start();

        await _gameRepository.CommitAsync();

        return new StartGameCommandResult
        {
            IsStart = true
        };
    }
}