using Application.Commands.Test;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.Test;

[Mapper]
public static partial class TestMapper
{
    public static partial TestCommand Map(this TestRequest request);
    public static partial TestResponse Map(this TestCommandResult result);
}