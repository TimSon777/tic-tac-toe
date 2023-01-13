using Application.Abstractions;

namespace Application.Commands.SignUp;

public sealed class SignUpHandler : CommandHandlerBase<SignUpCommand, SignUpResult>
{
    private readonly IUserManager _userManager;

    public SignUpHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }

    protected override async Task<SignUpResult> Handle(SignUpCommand command)
    {
        var result = await _userManager.CreateUserAsync(command.User, command.Password);
        return result.Map();
    }
}