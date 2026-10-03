using AgendaSyncer.Core.Exceptions;
using AgendaSyncer.Core.Interfaces;
using AgendaSyncer.Core.SyncEngine.Mappers;
using AgendaSyncer.Core.SyncEngine.Models.Syncer;
using AgendaSyncer.Core.SyncEngine.Utilities;
using Ical.Net.CalendarComponents;

namespace AgendaSyncer.Core.Services;

public class SyncEngineService
{
    private readonly ICalendarService<CalendarService, IList<Event>> _googleCalendarService = new GoogleCalendarService();
    private readonly ICalendarService<HttpResponseMessage, HttpResponseMessage> _appleCalendarService = new AppleCalendarService();

    
    
    public SyncEngineService()
    {
    }
    
    #region SyncEngine

    private SyncEventDto TransformEvent<TEvent>(object eventObject)
    {
        if (eventObject is not TEvent)
        {
            throw new SyncEventExeption("Given object is an invalid Event Type");
        }

        var mappedEvent = eventObject switch
        {
            Event googleEvent => EventMapper.MapGoogleEventToSyncEvent(googleEvent),
            _ => throw new SyncEventExeption("Failed to map event to SyncEventEntity.")
        };

        return mappedEvent;
    }
    
    public void SyncEvents()
    {
    }
    
    private void SendEventsToGoogle()
    {
        throw new NotImplementedException();
    }
    
    private void SendEventsToApple()
    {
        throw new NotImplementedException();
    }

    #endregion
    
    #region Google
    private CalendarService CreateConnectionToGoogle()
    {
        Log.Information("Creating connection to Google Calendar");
        return _googleCalendarService.CreateConnection();
    }

    public List<SyncEventDto> MapGoogleEvents()
    {
        CalendarService googleCalendar = CreateConnectionToGoogle();
        List<SyncEventDto> syncEvents = new List<SyncEventDto>();
        
        try
        {
            Log.Information("Retrieving Google Calendar Events");
            var googleEvents = _googleCalendarService.GetEvents(googleCalendar);
            
            Log.Information("Mapping Google Calendar Events to SyncEngine Events");
            foreach (var googleEvent in googleEvents)
            {
                SyncEventDto syncEvent = TransformEvent<Event>(googleEvent);
                syncEvents.Add(syncEvent);
            }

            return syncEvents;
        }
        catch (EventMapperException ex)
        {
            throw new EventMapperException("Unable to map Google Events to Sync Events", ex);
        }
    }

    #endregion
    
    #region Apple
    public HttpResponseMessage CreateConnectionToApple()
    {
        return _appleCalendarService.CreateConnection();
    }

    public List<CalendarEvent> GetAppleEvents()
    {
        List<CalendarEvent> appleEvents = new();
        
        Log.Information("Retrieving Apple Calendar Events");
        
        // TODO: Onderstaande logica verplaatsen naar AppleCalendarService
        HttpResponseMessage appleConnection = CreateConnectionToApple();
        string events = _appleCalendarService.GetEvents(appleConnection)
            .Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();
        
        IEnumerable<string> appleCalendars = AppleResponseParser.ParseToCalendars(events);

        foreach (string calendar in appleCalendars)
        {
            CalendarEvent appleEvent = AppleResponseParser.DeserializeToAppleEvent(calendar);
            appleEvents.Add(appleEvent);
        }

        return appleEvents;
    }
    
    #endregion
}