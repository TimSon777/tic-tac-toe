namespace Domain.Common;

public class BaseEntity
{ }

public class BaseEntity<TKey> : BaseEntity
    where TKey : notnull
{
    public TKey Id { get; set; } = default!;
}