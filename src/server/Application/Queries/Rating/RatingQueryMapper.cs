using Application.Queries.Rating.Objects;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Queries.Rating;

[Mapper]
public static partial class RatingQueryMapper
{
    public static partial IEnumerable<UserRatingObject> Map(this IEnumerable<User> users);
}