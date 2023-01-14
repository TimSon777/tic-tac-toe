namespace Application.Commands.Move;

public sealed class MoveCommand : ICommand<MoveCommandResult>
{
    public required string UserName { get; set; }
    public required int X { get; set; }
    public required int Y { get; set; }
}