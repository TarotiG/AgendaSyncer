using AgendaSyncer.Core.Interfaces;

namespace AgendaSyncer.Core.Abstractions;

public abstract class CalendarServiceBase<TConnection> : ICalendarService<TConnection>
{
    public abstract TConnection CreateConnection();
    public abstract void CreateEvent();
}