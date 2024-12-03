using Zeus.Api.Application.Interfaces.Services;

namespace Zeus.Api.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
