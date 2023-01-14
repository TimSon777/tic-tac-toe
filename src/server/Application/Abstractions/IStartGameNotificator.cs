namespace Application.Abstractions;

public interface IStartGameNotificator
{
    Task NotifyAsync(string initiatorUserName,
        string userName);
}