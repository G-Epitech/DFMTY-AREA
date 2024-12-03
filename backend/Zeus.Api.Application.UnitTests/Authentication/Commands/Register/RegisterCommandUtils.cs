using Zeus.Api.Application.Authentication.Commands.Register;
using Zeus.Api.Application.UnitTests.Authentication.TestsUtils.Constants.Interfaces;

namespace Zeus.Api.Application.UnitTests.Authentication.Commands.Register;

public static class RegisterCommandUtils
{
    public static RegisterCommand CreateRegisterCommand(ConstantUser? user = null) =>
        new RegisterCommand(
            user?.Email ?? "test@test.com",
            user?.Password ?? "test123;",
            user?.FirstName ?? "FirstName",
            user?.LastName ?? "LastName"
        );
}
