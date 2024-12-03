using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services;
using Zeus.Api.Domain.Errors;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Users.Query;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<GetUserQueryResult>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserSettingsProvider _userSettingsProvider;

    public GetUserQueryHandler(IUserReadRepository userReadRepository, IUserSettingsProvider userSettingsProvider)
    {
        _userReadRepository = userReadRepository;
        _userSettingsProvider = userSettingsProvider;
    }
    
    public async Task<ErrorOr<GetUserQueryResult>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetUserByIdAsync(new UserId(query.UserId));

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        return new GetUserQueryResult(
            user.Id.Value,
            user.Email,
            user.FirstName,
            user.LastName,
            _userSettingsProvider.DefaultPicture);
    }
}
