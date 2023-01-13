namespace Application.Commands.SigIn;

public sealed class SignInResult
{
    public bool Succeeded => !Errors.Any();
    public IEnumerable<string> Errors { get; set; } = new List<string>();
    public string AccessToken { get; set; } = default!;
}