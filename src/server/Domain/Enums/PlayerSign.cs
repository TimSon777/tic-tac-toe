namespace Domain.Enums;

public enum PlayerSign
{
    Nought = 0,
    Cross = 1
}

public static class PlayerSignExtensions
{
    public static string ToStr(this PlayerSign playerSign)
    {
        return playerSign == PlayerSign.Cross
            ? "X"
            : "O";
    }
}