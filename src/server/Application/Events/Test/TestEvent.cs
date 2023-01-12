namespace Application.Events.Test;

public sealed class TestEvent : IEvent
{
    public required int Number { get; set; }
}