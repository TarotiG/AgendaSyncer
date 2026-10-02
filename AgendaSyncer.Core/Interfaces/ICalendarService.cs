namespace AgendaSyncer.Core.Interfaces;

public interface ICalendarService<TConnection, TEvent>
{
    TConnection CreateConnection();
    TEvent GetEvents(TConnection connection);
    
    void CreateEvent();
}