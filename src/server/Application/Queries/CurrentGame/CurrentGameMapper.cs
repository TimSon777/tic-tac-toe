using Domain.Entities;
using Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace Application.Queries.CurrentGame;

[Mapper]
public static partial class CurrentGameMapper
{
    public static CurrentGameQueryResult Map(this Game game, string userName)
    {
        var user = game.Initiator.User.UserName == userName
            ? game.Initiator
            : game.Mate!;

        var mate = game.GetMateUserName(userName);
        
        return new CurrentGameQueryResult
        {
            Sign = user.PlayerSign,
            Board = game.Board.Moves.ToJagged(e => e.ToChar()),
            MateUserName = mate,
            GameStatus = game.Status
        };
    }
}