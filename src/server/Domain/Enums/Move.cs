namespace Domain.Enums;

public enum Move
{
    Nought,
    Cross,
    Empty
}

public static class MoveExtensions
{
    public static string ToStr(this Move move)
    {
        return move switch
        {
            Move.Cross => "X",
            Move.Nought => "O",
            Move.Empty => "_",
            _ => throw new ArgumentOutOfRangeException(nameof(move), move, null)
        };
    }
    
    public static Move FromChar(this char move)
    {
        return move switch
        {
            'X' => Move.Cross,
            'O' => Move.Nought,
            '_' => Move.Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(move), move, null)
        };
    }
}