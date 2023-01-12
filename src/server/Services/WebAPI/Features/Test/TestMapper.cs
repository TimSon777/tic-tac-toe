using Application.Commands.Test;
using Application.Queries.Test;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.Test;

[Mapper]
public static partial class TestMapper
{
    public static partial TestCommand MapToCommand(this TestRequest request);
    public static partial TestQuery MapToQuery(this TestRequest request);
}