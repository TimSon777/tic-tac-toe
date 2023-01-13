using Application.Commands.ConnectPlayer;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.ConnectPlayer;

[Mapper]
public static partial class Mapper
{
    public static partial Response Map(this ConnectPlayerCommandResult result);
}