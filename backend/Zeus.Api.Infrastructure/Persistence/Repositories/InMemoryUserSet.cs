using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public static class InMemoryUserSet
{
    public static List<User> Users { get; } = [];
}
