using AeroPackage.Application.Common.Interfaces.Services;

namespace AeroPackage.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;
}