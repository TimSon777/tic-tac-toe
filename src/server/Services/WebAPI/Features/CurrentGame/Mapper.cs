using Application.Queries.CurrentGame;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.CurrentGame;

[Mapper]
public static partial class Mapper
{
    public static partial Response Map(this CurrentGameQueryResult query);
}