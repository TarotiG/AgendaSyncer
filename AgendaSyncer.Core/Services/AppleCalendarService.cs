using System.Net;
using AgendaSyncer.Core.Abstractions;
using AgendaSyncer.Core.SyncEngine.Clients.AppleClient;
using AgendaSyncer.Core.SyncEngine.Utilities;
using Ical.Net.CalendarComponents;

namespace AgendaSyncer.Core.Services;

public class AppleCalendarService : CalendarServiceBase<HttpResponseMessage, List<CalendarEvent>>
{
    public override HttpResponseMessage CreateConnection()
    {
        AppleCalendarClient client = new AppleCalendarClient();
        return client.ValidateConnection().GetAwaiter().GetResult();
    }
    
    public override List<CalendarEvent> GetEvents(HttpResponseMessage appleConnection)
    {
        List<CalendarEvent> events = new();
        if (appleConnection.IsSuccessStatusCode)
        {
            AppleCalendarClient client = new AppleCalendarClient();
            var response = client.RetrieveCalendar().GetAwaiter().GetResult();
            var calendar =  response
                .Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();
            
            IEnumerable<string> appleCalendars = AppleResponseParser.ParseToCalendars(calendar);

            foreach (string calendarEvent in appleCalendars)
            {
                CalendarEvent appleEvent = AppleResponseParser.DeserializeToAppleEvent(calendarEvent);
                events.Add(appleEvent);
            }
        }

        return events;
    }
    
    public override void CreateEvent()
    {
        throw new NotImplementedException();
    }
}