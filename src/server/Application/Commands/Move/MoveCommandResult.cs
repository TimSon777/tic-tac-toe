using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.Move;

public sealed class MoveCommandResult
{
    [MemberNotNullWhen(false, nameof(Error))]
    public required bool IsSuccess { get; set; }
    
    public string? Error { get; set; }

    public string MateUserName { get; set; } = default!;
    public string GameStatus { get; set; } = default!;
}