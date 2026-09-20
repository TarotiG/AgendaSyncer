using AgendaSyncer.Core.Abstractions;

namespace AgendaSyncer.Core.Services;

public class AppleCalendarService : CalendarServiceBase<object>
{
    public override object CreateConnection()
    {
        throw new NotImplementedException();
    }
    
    public override void CreateEvent()
    {
        throw new NotImplementedException();
    }
}