using TaskTracker.Application.Contracts.DateAndTime;

namespace TaskTracker.Infrastructure.DateAndTime
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
