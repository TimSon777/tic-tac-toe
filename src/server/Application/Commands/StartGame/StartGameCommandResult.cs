using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.StartGame;

public sealed class StartGameCommandResult
{
    [MemberNotNullWhen(true, nameof(InitiatorUserName), nameof(InitiatorPlayerSign), nameof(PlayerSign))]
    public required bool IsStart { get; set; }
    public string? InitiatorUserName { get; set; }
    public string? InitiatorPlayerSign { get; set; }
    public string? PlayerSign { get; set; }
}