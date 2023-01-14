using Application.Abstractions.Repositories;

namespace Application.Queries.Rating;

public sealed class RatingQueryHandler : QueryHandlerBase<RatingQuery, RatingQueryResult>
{
    private readonly IUserRepository _userRepository;

    public RatingQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task<RatingQueryResult> Handle(RatingQuery command)
    {
        var users = await _userRepository.ListUsersWithHighestRatingAsync(command.ItemsNumber);

        return new RatingQueryResult
        {
            UserRatings = users.Map()
        };
    }
}