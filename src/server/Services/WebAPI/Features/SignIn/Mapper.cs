using Application.Commands.SigIn;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.SignIn;

[Mapper]
public static partial class Mapper
{
    public static partial SignInCommand MapToCommand(this Request request);

    public static partial Response MapToResponse(this SignInResult result);
}