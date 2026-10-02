using System.Net;
using AgendaSyncer.Core.Abstractions;
using AgendaSyncer.Core.SyncEngine.Clients.AppleClient;

namespace AgendaSyncer.Core.Services;

public class AppleCalendarService : CalendarServiceBase<HttpResponseMessage, HttpResponseMessage>
{
    public override HttpResponseMessage CreateConnection()
    {
        AppleCalendarClient client = new AppleCalendarClient();
        return client.ValidateConnection().GetAwaiter().GetResult();
    }
    
    public override HttpResponseMessage GetEvents(HttpResponseMessage appleConnection)
    {
        if (appleConnection.IsSuccessStatusCode)
        {
            AppleCalendarClient client = new AppleCalendarClient();
            return client.RetrieveCalendar().GetAwaiter().GetResult();
        }

        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
    
    public override void CreateEvent()
    {
        throw new NotImplementedException();
    }
}