using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.Errors;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Application.Authentication.Commands.PasswordRegister;

public class PasswordAuthRegisterCommandHandler : IRequestHandler<PasswordAuthRegisterCommand, ErrorOr<PasswordAuthRegisterCommandResult>>
{
    private readonly IAuthenticationMethodWriteRepository _authenticationMethodWriteRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public PasswordAuthRegisterCommandHandler(IJwtGenerator jwtGenerator, IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository,
        IAuthenticationMethodWriteRepository authenticationMethodWriteRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _authenticationMethodWriteRepository = authenticationMethodWriteRepository;
    }

    public async Task<ErrorOr<PasswordAuthRegisterCommandResult>> Handle(PasswordAuthRegisterCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userReadRepository.GetUserByEmailAsync(command.Email, cancellationToken) is not null)
        {
            return Errors.Authentication.DuplicatedEmail;
        }

        var user = User.Create(command.FirstName, command.LastName, command.Email);
        var password = Password.Create(command.Password);
        var passwordAuthMethod = PasswordAuthenticationMethod.Create(user.Id, password);

        await _userWriteRepository.AddUserAsync(user, cancellationToken);

        try
        {
            await _authenticationMethodWriteRepository.AddAuthenticationMethodAsync(passwordAuthMethod,
                cancellationToken);
        }
        catch (Exception)
        {
            await _userWriteRepository.DeleteUserAsync(user, cancellationToken);
            throw;
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return new PasswordAuthRegisterCommandResult(accessToken, refreshToken, user.Id.Value);
    }
}
