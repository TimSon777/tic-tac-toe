using Domain.Common;
using Domain.Enums;
using Domain.Models;

namespace Domain.Entities;

public sealed class Game : BaseEntity<int>
{
    public Player Initiator { get; set; } = default!;
    public Player? Mate { get; set; }
    public Board Board { get; set; } = Board.Empty;
    public GameStatus Status { get; set; } = GameStatus.NotStarted;
    public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;

    public PlayerSign NextMove => GetNextMove();
    public bool IsMoveLefts => Board.Any(m => m == Enums.Move.Nought);

    public bool IsCorrectCoordinates(int x, int y)
    {
        return IsCorrectCoordinate(x) && IsCorrectCoordinate(y);
    }

    private static bool IsCorrectCoordinate(int coordinate)
    {
        return coordinate is >= 0 and < Board.Size;
    }

    public void Start()
    {
        var initiatorSign = Random.Shared.Next(0, 2) == 0 
            ? PlayerSign.Cross
            : PlayerSign.Nought;

        var mateSign = initiatorSign == PlayerSign.Cross
            ? PlayerSign.Nought
            : PlayerSign.Cross;

        Initiator.PlayerSign = initiatorSign;
        Mate!.PlayerSign = mateSign;
        
        Status = GameStatus.InProgress;
    }

    public bool IsGameOver => GetCurrentStatus() is GameStatus.Draw or GameStatus.CrossWin or GameStatus.NoughtWin;
    
    public MoveResult Move(int x, int y, string userName)
    {
        var playerSign = Mate!.User.UserName == userName
            ? Mate.PlayerSign
            : Initiator.PlayerSign;

        if (GetCurrentStatus() != GameStatus.InProgress)
        {
            return MoveResult.GameOver;
        }
        
        if (!IsMoveLefts)
        {
            return MoveResult.BoardAlreadyFilled;
        }

        if (playerSign != NextMove)
        {
            return MoveResult.WrongSign;
        }

        if (Board[x, y] != Enums.Move.Nought)
        {
            return MoveResult.NotEmpty;
        }

        Board[x, y] = playerSign == PlayerSign.Cross
            ? Enums.Move.Cross
            : Enums.Move.Nought;
        
        return MoveResult.Ok;
    }

    public string GetMateUserName(string userName)
    {
        return Mate!.User.UserName == userName
            ? Initiator.User.UserName!
            : Mate.User.UserName!;
    }
    
    public GameStatus GetCurrentStatus()
    {
        for(var x = 0; x < 3; x++)
        {
            if(Board[x, 0] != Enums.Move.Empty && Board[x, 0] == Board[x, 1] && Board[x, 1] == Board[x, 2])
            {
                return GetStatus(Board[x, 0]);
            }
        }

        for(var y = 0; y < 3; y++)
        {
            if(Board[0, y] != Enums.Move.Empty && Board[0, y] == Board[1, y] && Board[1, y] == Board[2, y])
            {
                return GetStatus(Board[0, y]);
            }
        }

        if(Board[0, 0] != Enums.Move.Empty && Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
        {
            return GetStatus(Board[0, 0]);
        }

        if(Board[0, 2] != Enums.Move.Empty && Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
        {
            return GetStatus(Board[0, 2]);
        }

        return IsMoveLefts
            ? GameStatus.InProgress
            : GameStatus.Draw;
    }
    
    private PlayerSign GetNextMove()
    {
        var crossCount = Board.Count(m => m == Enums.Move.Cross);
        var noughtCount = Board.Count(m => m == Enums.Move.Nought);

        return crossCount == noughtCount
            ? PlayerSign.Cross
            : PlayerSign.Nought;
    }

    private static GameStatus GetStatus(Move move)
    {
        return move == Enums.Move.Cross
            ? GameStatus.CrossWin
            : GameStatus.NoughtWin;
    }
}