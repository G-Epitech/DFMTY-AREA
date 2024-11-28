using Zeus.Api.Application.Common.Interfaces.Services;

namespace Zeus.Api.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
