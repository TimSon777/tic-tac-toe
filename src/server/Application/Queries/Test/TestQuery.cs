namespace Application.Queries.Test;

public sealed class TestQuery : IQuery<TestQueryResult>
{
    public required int Number { get; set; }
}