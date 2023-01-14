namespace Domain.Enums;

public enum MoveResult
{
    Ok = 0,
    BoardAlreadyFilled = 1,
    WrongSign = 2,
    NotEmpty = 3,
    GameOver = 4,
}