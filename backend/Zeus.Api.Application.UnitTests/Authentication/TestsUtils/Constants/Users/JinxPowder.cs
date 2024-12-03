using Zeus.Api.Application.UnitTests.Authentication.TestsUtils.Constants.Interfaces;

namespace Zeus.Api.Application.UnitTests.Authentication.TestsUtils.Constants.Users;

public static partial class Constants
{
    public static partial class Users
    {
        public static ConstantUser JinxPowder() => new ConstantUser(
            "jinxpowder@riot.com",
            "Il0veVander;",
            "Jinx",
            "Powder",
            "https://i.pinimg.com/564x/70/81/bb/7081bbdbf3fe3bf3d8b5ee4b0c2a37c8.jpg");
    }
}
