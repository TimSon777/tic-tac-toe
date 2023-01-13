namespace Application.Commands.SignUp;

public sealed class SignUpResult
{
    public bool Succeeded => !Errors.Any();
    public IEnumerable<string> Errors { get; set; } = default!;
}