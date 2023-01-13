namespace Application.Commands.SignUp;

public sealed class SignUpCommand : ICommand<SignUpResult>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}