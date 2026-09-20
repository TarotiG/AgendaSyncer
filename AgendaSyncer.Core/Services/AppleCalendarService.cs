using AgendaSyncer.Core.Abstractions;
using AgendaSyncer.Core.SyncEngine.Clients.AppleClient;

namespace AgendaSyncer.Core.Services;

public class AppleCalendarService : CalendarServiceBase<string>
{
    public override string CreateConnection()
    {
        AppleCalendarClient client = new AppleCalendarClient();
        return client.RetrieveCalendar().GetAwaiter().GetResult();
    }
    
    public override void CreateEvent()
    {
        throw new NotImplementedException();
    }
}