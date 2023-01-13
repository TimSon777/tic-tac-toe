using Domain.Entities;

namespace Application.Commands.SignUp;

public sealed class SignUpCommand : ICommand<SignUpResult>
{
    public required IUser User { get; set; }
    public required string Password { get; set; }
}