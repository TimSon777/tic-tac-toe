namespace Domain.Entities;

public sealed class User : IUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }
}