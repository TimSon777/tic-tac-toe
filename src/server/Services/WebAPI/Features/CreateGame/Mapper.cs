using Application.Commands.CreateGame;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.CreateGame;

[Mapper]
public static partial class Mapper
{
    public static partial Response Map(this CreateGameCommandResult result);
}