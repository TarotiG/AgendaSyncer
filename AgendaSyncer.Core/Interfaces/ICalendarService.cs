namespace AgendaSyncer.Core.Interfaces;

public interface ICalendarService<TConnection>
{
    TConnection CreateConnection();
    
    void CreateEvent();
}