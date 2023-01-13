using Application.Abstractions;

namespace Application.Commands.SigIn;

public sealed class SignInHandler : CommandHandlerBase<SignInCommand, SignInResult>
{
    private readonly IUserManager _userManager;
    private readonly IJwtProvider _jwtProvider;

    public SignInHandler(IUserManager userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    protected override async Task<SignInResult> Handle(SignInCommand command)
    {
        var isCorrect = await _userManager.CheckCorrectAsync(command.UserName, command.Password);

        if (!isCorrect)
        {
            return new SignInResult
            {
                Errors = new List<string>
                {
                    "User Name or Password is / are incorrect"
                }
            };
        }

        var user = await _userManager.GetByUserNameAsync(command.UserName);
        return _jwtProvider.Generate(user).Map();
    }
}