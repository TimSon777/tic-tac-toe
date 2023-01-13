using Domain.Common;

namespace Domain.Entities;

public interface IUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }
}

public class User : BaseEntity<int>, IUser
{
    public string? UserName { get; set; }
}