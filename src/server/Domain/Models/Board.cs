﻿using Domain.Enums;

namespace Domain.Models;

public sealed class Board
{
    public Move[,] Moves { get; set; } = default!;

    public const int Size = 3;
    
    public Move this[int x, int y]
    {
        get => Moves[x, y];
        set => Moves[x, y] = value;
    }

    public static Board Empty = GetEmpty();

    private static Board GetEmpty()
    {
        const Move move = Move.Empty;

        var moves = new Move[Size, Size];

        for (var x = 0; x < Size; x++)
        {
            for (var y = 0; y < Size; y++)
            {
                moves[x, y] = move;
            }
        }

        return new Board
        {
            Moves = moves
        };
    }
}