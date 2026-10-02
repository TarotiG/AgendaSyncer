using AgendaSyncer.Core.Interfaces;

namespace AgendaSyncer.Core.Abstractions;

public abstract class CalendarServiceBase<TConnection, TEvent> : ICalendarService<TConnection, TEvent>
{
    public abstract TConnection CreateConnection();
    public abstract TEvent GetEvents(TConnection connection);
    public abstract void CreateEvent();
}