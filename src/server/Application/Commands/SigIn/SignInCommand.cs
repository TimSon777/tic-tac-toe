namespace Application.Commands.SigIn;

public sealed class SignInCommand : ICommand<SignInResult>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}