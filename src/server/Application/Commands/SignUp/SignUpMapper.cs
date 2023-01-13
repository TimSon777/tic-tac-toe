using Application.Models;
using Riok.Mapperly.Abstractions;

namespace Application.Commands.SignUp;

[Mapper]
public static partial class SignUpMapper
{
    public static partial SignUpResult Map(this AccountingResult result);
}