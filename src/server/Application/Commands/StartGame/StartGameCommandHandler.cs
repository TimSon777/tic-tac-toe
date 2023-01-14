﻿using Application.Abstractions.Repositories;
using Domain.Enums;

namespace Application.Commands.StartGame;

public sealed class StartGameCommandHandler : CommandHandlerBase<StartGameCommand, StartGameCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;

    public StartGameCommandHandler(IUserRepository userRepository, IGameRepository gameRepository, IPlayerRepository playerRepository)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
    }

    protected override async Task<StartGameCommandResult> Handle(StartGameCommand command)
    {
        var user = await _userRepository.FindByUserNameWithReadyPlayerAsync(command.UserName);
        
        if (user is null)
        {
            return new StartGameCommandResult
            {
                IsStart = false
            };
        }

        var game = await _gameRepository.GetGameByUserNameAsync(user.UserName!);
        
        game.Start();

        await _gameRepository.CommitAsync();

        var player = await _playerRepository.GetInitiatorAsync(user.UserName!);
        
        return new StartGameCommandResult
        {
            IsStart = true,
            InitiatorUserName = player.User.UserName,
            InitiatorPlayerSign = game.Initiator.PlayerSign.ToChar().ToString(),
            PlayerSign = game.Mate!.PlayerSign.ToChar().ToString()
        };
    }
}