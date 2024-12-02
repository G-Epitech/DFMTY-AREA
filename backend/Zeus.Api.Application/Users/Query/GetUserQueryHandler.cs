using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.UserAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Users.Query;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<GetUserQueryResult>>
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserQueryHandler(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }


    public Task<ErrorOr<GetUserQueryResult>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = _userReadRepository.GetUserById(new UserId(query.UserId));

        if (user is null)
        {
            return Task.FromResult<ErrorOr<GetUserQueryResult>>(Errors.User.NotFound);
        }

        return Task.FromResult<ErrorOr<GetUserQueryResult>>(new GetUserQueryResult(
            user.Id.Value,
            user.Email,
            user.FirstName,
            user.LastName));
    }
}
