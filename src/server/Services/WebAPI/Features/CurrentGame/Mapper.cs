using Application.Queries.CurrentGame;
using Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.CurrentGame;

[Mapper]
public static partial class Mapper
{
    public static Response Map(this CurrentGameQueryResult query)
    {
        return new Response
        {
            Board = query.Board,
            Sign = query.Sign.ToStr(),
            GameStatus = query.GameStatus.ToString(),
            MateUserName = query.MateUserName
        };
    }
}