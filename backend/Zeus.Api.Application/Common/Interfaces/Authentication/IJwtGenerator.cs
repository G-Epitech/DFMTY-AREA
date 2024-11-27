using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string Generate(User user);
}
