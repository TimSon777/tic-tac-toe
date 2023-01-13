namespace WebAPI.Features.SignIn;

public sealed class Response
{
    public required bool Succeeded { get; set; }
    public required IEnumerable<string> Errors { get; set; }
    public required string AccessToken { get; set; }
}