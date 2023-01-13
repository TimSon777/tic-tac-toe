using Application.Commands.SignUp;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.SignUp;

[Mapper]
public static partial class Mapper
{
    public static SignUpCommand MapToCommand(this Request request)
    {
        return new SignUpCommand
        {
            User = new User
            {
                UserName = request.UserName
            },
            Password = request.Password
        };
    }

    public static partial Response MapToResponse(this SignUpResult result);
}