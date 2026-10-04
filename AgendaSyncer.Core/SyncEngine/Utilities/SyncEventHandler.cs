using AgendaSyncer.Core.SyncEngine.Models.Syncer;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class SyncEventHandler
{
    public static void CompareEvents(List<SyncEventDto> googleEvents, List<SyncEventDto> appleEvents)
    {
        var query = googleEvents
            .Where(googleEvent => appleEvents.All(appleId => appleId.Id != googleEvent.ICalUID))
            .ToList();
        
        Log.Information("Events to be created: {Events}", query.Count);
    }
    
    public static void CreateEvent<TEvent>()
    {
    }

    private static void UpdateEvent<TEvent>()
    {
    }
}