using Application.Queries.Rating;
using Riok.Mapperly.Abstractions;

namespace WebAPI.Features.Rating;

[Mapper]
public static partial class Mapper
{
    public static partial Response Map(this RatingQueryResult result);
}