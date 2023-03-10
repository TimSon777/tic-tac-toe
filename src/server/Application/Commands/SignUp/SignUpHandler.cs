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
        var user = await _userManager.FindByUserNameAsync(command.UserName);

        if (user is not null)
        {
            return new SignUpResult
            {
                Errors = new List<string>
                {
                    $"User by user name {command.UserName} already exists"
                }
            };
        }
        
        var result = await _userManager.CreateUserAsync(command.UserName, command.Password);
        return result.Map();
    }
}