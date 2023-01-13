namespace WebAPI.Features.Gaming;

public interface IGamingClient
{
    Task IsConnected(bool result);
    Task IsConnected(string userName);
}