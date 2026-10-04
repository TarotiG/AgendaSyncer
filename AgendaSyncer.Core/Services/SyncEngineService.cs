using AgendaSyncer.Core.Exceptions;
using AgendaSyncer.Core.Interfaces;
using AgendaSyncer.Core.SyncEngine.Mappers;
using AgendaSyncer.Core.SyncEngine.Models.Syncer;
using AgendaSyncer.Core.SyncEngine.Utilities;
using Ical.Net.CalendarComponents;
using EventHandler = System.EventHandler;

namespace AgendaSyncer.Core.Services;

public class SyncEngineService
{
    private readonly ICalendarService<CalendarService, IList<Event>> _googleCalendarService = new GoogleCalendarService();
    private readonly ICalendarService<HttpResponseMessage, List<CalendarEvent>> _appleCalendarService = new AppleCalendarService();

    
    
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
            CalendarEvent appleEvent => EventMapper.MapAppleEventToSyncEventDto(appleEvent),
            _ => throw new SyncEventExeption("Failed to map event to SyncEventEntity.")
        };

        return mappedEvent;
    }
    
    public void SyncEvents()
    {
        List<SyncEventDto> googleEvents = MapGoogleEvents();
        List<SyncEventDto> appleEvents = MapAppleEvents();
        
        foreach (SyncEventDto syncEvent in googleEvents)
        {
            Log.Information("Syncing Google Event: {EventTitle}\nDatum: {EventDate}",
                syncEvent.Title,
                syncEvent.StartDateTime.DateTimeDateTimeOffset);
        }

        foreach (SyncEventDto syncEvent in appleEvents)
        {
            Log.Information("Syncing Apple Event: {Event}\nDatum: {EventDate}",
                syncEvent.Title,
                syncEvent.StartDateTime.DateTimeDateTimeOffset);
        }

        SyncEventHandler.CompareEvents(googleEvents, appleEvents);
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
            
            foreach (Event googleEvent in googleEvents)
            {
                Log.Information("Syncing Google Event: {Event}\nEvent Type: {EventType}\nEvent Properties: {EventProps}",
                    googleEvent.Status,
                    googleEvent.EventType,
                    googleEvent.ExtendedProperties.Private__);
            }
            
            Log.Information("Mapping Google Calendar Events to SyncEngine Events");
            foreach (Event googleEvent in googleEvents)
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

    public List<SyncEventDto> MapAppleEvents()
    {
        List<SyncEventDto> syncEvents = new();
        
        Log.Information("Retrieving Apple Calendar Events");
        try
        {
            HttpResponseMessage appleConnection = CreateConnectionToApple();
            List<CalendarEvent> events = _appleCalendarService.GetEvents(appleConnection);
            
            Log.Information("Mapping Apple Calendar Events to SyncEngine Events");
            foreach (CalendarEvent appleEvent in events)
            {
                SyncEventDto syncEvent = TransformEvent<CalendarEvent>(appleEvent);
                syncEvents.Add(syncEvent);
            }

            return syncEvents;
        }
        catch (SyncEngineApplicationException e)
        {
            throw new SyncEngineApplicationException("Unable to map Apple Events to SyncEngine Events", e);
        }
    }
    
    #endregion
}