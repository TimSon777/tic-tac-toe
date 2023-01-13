using Application.Commands.SignUp;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.SignUp;

[Mapper]
public static partial class Mapper
{
    public static partial SignUpCommand MapToCommand(this Request request);

    public static partial Response MapToResponse(this SignUpResult result);
}