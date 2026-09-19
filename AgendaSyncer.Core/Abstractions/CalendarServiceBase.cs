namespace AgendaSyncer.Core.Abstractions;

public abstract class CalendarServiceBase
{
    public abstract void CreateConnection();
    
    public abstract void GetEvents();
}