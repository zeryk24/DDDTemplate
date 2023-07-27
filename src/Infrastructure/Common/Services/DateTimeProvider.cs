using Application.Common.Services;
using Domain.Common.Attributes;

namespace Infrastructure.Common.Services;

[Register<IDateTimeProvider>]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
}
