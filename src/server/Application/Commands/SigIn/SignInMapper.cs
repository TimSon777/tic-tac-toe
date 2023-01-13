using Application.Models;
using Riok.Mapperly.Abstractions;

namespace Application.Commands.SigIn;

[Mapper]
public static partial class SignInMapper
{
    public static partial SignInResult Map(this JwtResult result);
}