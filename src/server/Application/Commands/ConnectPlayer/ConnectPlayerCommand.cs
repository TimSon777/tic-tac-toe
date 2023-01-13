namespace Application.Commands.ConnectPlayer;

public sealed class ConnectPlayerCommand : ICommand<ConnectPlayerCommandResult>
{
    public required string UserName { get; set; }
    public required string InitiatorUserName { get; set; }
}