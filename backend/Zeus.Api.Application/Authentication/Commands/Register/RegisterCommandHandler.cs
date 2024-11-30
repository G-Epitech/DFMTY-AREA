using ErrorOr;

using MediatR;

using Zeus.Api.Application.Authentication.Common;
using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Authentication;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public RegisterCommandHandler(IJwtGenerator jwtGenerator, IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
    }

    public Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        if (_userReadRepository.GetUserByEmail(command.Email) is not null)
        {
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.Authentication.DuplicatedEmail);
        }

        var user = User.Create(command.FirstName, command.LastName, command.Email, command.Password);
        _userWriteRepository.AddUser(user);

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);
        
        return Task.FromResult<ErrorOr<AuthenticationResult>>(new AuthenticationResult(accessToken, refreshToken));
    }
}
