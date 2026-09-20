using AgendaSyncer.Core.SyncEngine.Utilities;

namespace AgendaSyncer.Core.SyncEngine.Clients.AppleClient;

public class AppleCalendarClient
{
    public AppleCalendarClient()
    {
        SecretsConfig.Load();
    }
    
    public async Task<string> RetrieveCalendar()
    {
        using HttpClient httpClient = new HttpClient();
        return await HttpHandler.SendPropFindRequest(
            httpClient,
            Environment.GetEnvironmentVariable("APPLE_PROPFIND_URL")!,
            Environment.GetEnvironmentVariable("APPLE_USER")!,
            Environment.GetEnvironmentVariable("APPLE_SPEC_PW")!
            );
    }
}