namespace Domain.Entities;

public interface IUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }
}