using AgendaSyncer.Core.Exceptions;
using AgendaSyncer.Core.Interfaces;
using AgendaSyncer.Core.SyncEngine.Models;

namespace AgendaSyncer.Core.Services;

public class SyncEngineService
{
    private readonly IGoogleCalendarService _googleCalendarService = new GoogleCalendarService();
    private readonly IAppleCalendarService _appleCalendarService = new AppleCalendarService();
    
    private CalendarService _googleCalendar;
    
    
    public SyncEngineService()
    {
        _googleCalendar = CreateConnectionToGoogle();
    }
    
    #region SyncEngine

    private SyncEventEntity TransformEvent<TEvent>()
    {
        return new SyncEventEntity();
    }
    
    public void SyncEvents()
    {
    }

    #endregion
    
    #region Google
    private CalendarService CreateConnectionToGoogle()
    {
        Log.Information("Creating connection to Google Calendar");
        return _googleCalendarService.CreateConnection();
    }

    public List<SyncEventEntity> MapGoogleEvents()
    {
        List<SyncEventEntity> syncEvents = new();
        
        try
        {
            Log.Information("Retrieving Google Calendar Events");
            var googleEvents = _googleCalendar.Events.List("primary").Execute().Items;
            
            Log.Information("Mapping Google Calendar Events to SyncEngine Events");
            foreach (var googleEvent in googleEvents)
            {
                SyncEventEntity syncEvent = TransformEvent<Event>();
                syncEvents.Add(syncEvent);
            }

            return syncEvents;
        }
        catch (SyncEventExeption ex)
        {
            throw new SyncEventExeption("Unable to map Google Events to Sync Events", ex);
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