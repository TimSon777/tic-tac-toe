namespace WebAPI.Features.Gaming;

public interface IGamingClient
{
    Task IsConnected(string userName, string sign);
    Task MateMoved(int x, int y, string? error);
    Task GameOverWhenDisconnect();
    Task GameOver(string gameStatus);
}