namespace WebAPI.Features.SignUp;

public sealed class Response
{
    public required bool Succeeded { get; set; }
    public required IEnumerable<string> Errors { get; set; }
}