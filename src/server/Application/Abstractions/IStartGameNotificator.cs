namespace Application.Abstractions;

public interface IStartGameNotificator
{
    Task NotifyAsync(
        bool isConnect,
        string initiatorUserName,
        string userName);
}