namespace WebAPI.Features.Gaming;

public interface IGamingClient
{
    Task IsConnected(string userName);
    Task UserMoved(bool isSuccess, string? error, string gameStatus);
    Task MateMoved(int x, int y, string gameStatus);
}