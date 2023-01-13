using Application.Queries.CurrentGames;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.CurrentGames;

[Mapper]
public static partial class Mapper
{
    public static partial CurrentGamesQuery Map(this Request request);

    public static partial Response Map(this CurrentGamesQueryResult result);
}