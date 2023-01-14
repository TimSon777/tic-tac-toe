using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity<int>, IUser
{
    public string? UserName { get; set; }
    public int Rating { get; set; }
}