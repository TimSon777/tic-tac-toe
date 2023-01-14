namespace WebAPI.Features.Gaming;

public interface IGamingClient
{
    Task IsConnected(string userName);
    Task MateMoved(int x, int y, string gameStatus);
    Task GameOver(bool isCancelled);
}