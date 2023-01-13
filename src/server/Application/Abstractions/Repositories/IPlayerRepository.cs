﻿using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IPlayerRepository
{
    Task<Player> CreatePlayerAsync(string userName);
}