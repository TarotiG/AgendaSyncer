using AgendaSyncer.Core.Services;
using AgendaSyncer.Core.SyncEngine.Models.Syncer;
using Google.Apis.Http;

namespace AgendaSyncer.Core.SyncEngine;

/// <summary>
/// Retrieves data from either a Google calendar or an Apple calendar and syncs the data to the other calendar
/// if data doesn't match with the opposing calendar.
/// </summary>
public class SyncEngine
{
    public static void ConnectToCalendars()
    {
        SyncEngineService syncEngineService = new();
        // List<SyncEventDto> syncEvents = syncEngineService.MapGoogleEvents();
        
        syncEngineService.CreateConnectionToApple();
    }
    
    public static void SyncCalendars()
    {
    }

    private static void ReceiveEvents()
    {
    }
    
    private static void SendGoogleEvents()
    {
    }
    private static void SendAppleEvents()
    {
    }
}