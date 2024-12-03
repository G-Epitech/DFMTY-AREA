using FluentAssertions;

using Moq;

using Zeus.Api.Application.Authentication.Commands.Register;
using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.UnitTests.Authentication.TestsUtils.Constants.Users;

namespace Zeus.Api.Application.UnitTests.Authentication.Commands.Register;

public class RegisterCommandHandlerTests
{
    private readonly RegisterCommandHandler _handler;
    private readonly Mock<IJwtGenerator> _mockJwtGeneratorMock;
    private readonly Mock<IUserReadRepository> _mockUserReadRepository;
    private readonly Mock<IUserWriteRepository> _mockUserWriteRepository;

    public RegisterCommandHandlerTests()
    {
        _mockJwtGeneratorMock = new Mock<IJwtGenerator>();
        _mockUserWriteRepository = new Mock<IUserWriteRepository>();
        _mockUserReadRepository = new Mock<IUserReadRepository>();
        _handler = new RegisterCommandHandler(_mockJwtGeneratorMock.Object, _mockUserReadRepository.Object, _mockUserWriteRepository.Object);
    }
    
    [Fact]
    public async Task HandlerRegisterCommand_WhenAccountIsValid_ShouldCreateAndReturnTokens()
    {
        var registerCommand = RegisterCommandUtils.CreateRegisterCommand(Constants.Users.JinxPowder());

        var commandResult = await _handler.Handle(registerCommand, default);
        
        commandResult.IsError.Should().BeFalse();
        commandResult.Value.ValidateTokensFrom(registerCommand);
    }
}
 
