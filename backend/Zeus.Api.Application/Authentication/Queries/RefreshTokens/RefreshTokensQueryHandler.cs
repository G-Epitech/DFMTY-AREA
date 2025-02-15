﻿using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Errors;

namespace Zeus.Api.Application.Authentication.Queries.RefreshTokens;

public class RefreshTokensQueryHandler : IRequestHandler<RefreshTokensQuery, ErrorOr<RefreshTokensQueryResult>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserReadRepository _userReadRepository;

    public RefreshTokensQueryHandler(IUserReadRepository userReadRepository, IJwtGenerator jwtGenerator)
    {
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<ErrorOr<RefreshTokensQueryResult>> Handle(RefreshTokensQuery request, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return new RefreshTokensQueryResult(accessToken, refreshToken);
    }
}
