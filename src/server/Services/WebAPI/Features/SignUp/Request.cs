namespace WebAPI.Features.SignUp;

public sealed class Request
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}