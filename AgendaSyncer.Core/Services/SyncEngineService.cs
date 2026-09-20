using AgendaSyncer.Core.Exceptions;
using AgendaSyncer.Core.Interfaces;
using AgendaSyncer.Core.SyncEngine.Mappers;
using AgendaSyncer.Core.SyncEngine.Models.Syncer;

namespace AgendaSyncer.Core.Services;

public class SyncEngineService
{
    private readonly ICalendarService<CalendarService> _googleCalendarService = new GoogleCalendarService();
    private readonly ICalendarService<object> _appleCalendarService = new AppleCalendarService();
    
    private readonly CalendarService _googleCalendar;
    
    
    public SyncEngineService()
    {
        _googleCalendar = CreateConnectionToGoogle();
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
        List<SyncEventDto> syncEvents = new List<SyncEventDto>();
        
        try
        {
            Log.Information("Retrieving Google Calendar Events");
            var googleEvents = _googleCalendar.Events.List("primary").Execute().Items;
            
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
    private void CreateConnectionToApple()
    {
    }

    public void GetAppleEvents()
    {
    }
    
    #endregion
}