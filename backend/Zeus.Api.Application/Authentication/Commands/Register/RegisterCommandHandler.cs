using ErrorOr;

using MediatR;

using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Authentication;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterCommandResult>>
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

    public async Task<ErrorOr<RegisterCommandResult>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userReadRepository.GetUserByEmailAsync(command.Email) is not null)
        {
            return Errors.Authentication.DuplicatedEmail;
        }

        var user = User.Create(command.FirstName, command.LastName, command.Email, command.Password);
        await _userWriteRepository.AddUserAsync(user);

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);
        
        return new RegisterCommandResult(accessToken, refreshToken, user.Id);
    }
}
