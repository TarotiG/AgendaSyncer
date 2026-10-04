using AgendaSyncer.Core.Abstractions;
using AgendaSyncer.Core.Exceptions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace AgendaSyncer.Core.Services;

public class GoogleCalendarService : CalendarServiceBase<CalendarService, IList<Event>>
{
    private CalendarService calendarService => CreateConnection();
    
    public GoogleCalendarService()
    {
    }
    
    public override CalendarService CreateConnection()
    {
        try
        {
            GoogleCredential credential = CreateCredentials(cancellationToken: CancellationToken.None)
                .GetAwaiter()
                .GetResult();
            
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "AgendaSync"
            });
        }
        catch (CalendarConnectionException e)
        {
            throw new CalendarConnectionException("Unable to connect to Google Calendar", e);
        }
    }
    
    public override IList<Event> GetEvents(CalendarService calendarService)
    {
        var request = calendarService.Events.List("primary");
        
        request.TimeMinDateTimeOffset = DateTimeOffset.UtcNow;
        request.TimeMaxDateTimeOffset = DateTimeOffset.UtcNow.AddMonths(1);
        request.SingleEvents = true;
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
        
        return  request.Execute().Items;
    }
    
    public override void CreateEvent()
    {
        throw new NotImplementedException();
    }

    // public async Task<int> DeleteSyncedEventsAsync(CalendarService service, string calendarId, bool dryRun = true)
    // {
    //     string? pageToken = null;
    //     int matched = 0, deleted = 0;
    //
    //     do
    //     {
    //         var request = service.Events.List(calendarId);
    //         request.ShowDeleted = false;      // al verwijderde events hoef je niet opnieuw te verwijderen
    //         request.SingleEvents = false;     // terugkerende reeksen als één event; verwijder je de reeks, dan gaan de instanties mee
    //         request.MaxResults = 250;
    //         request.PageToken = pageToken;
    //
    //         var response = await request.ExecuteAsync();
    //
    //         foreach (var ev in response.Items ?? Enumerable.Empty<Event>())
    //         {
    //             var syncId = ev.ExtendedProperties?.Private__?.GetValueOrDefault("syncId");
    //             if (syncId == null) continue;
    //
    //             // Optioneel strenger: alleen events die jouw syncer heeft gemaakt
    //             // if (!syncId.StartsWith("apple_")) continue;
    //
    //             matched++;
    //             Console.WriteLine($"{(dryRun ? "[DRY RUN] " : "")}Delete {ev.Id} | {ev.Summary} | {ev.Start?.DateTimeRaw ?? ev.Start?.Date} | {syncId}");
    //
    //             if (!dryRun)
    //             {
    //                 await DeleteWithRetryAsync(service, calendarId, ev.Id);
    //                 deleted++;
    //             }
    //         }
    //
    //         pageToken = response.NextPageToken;
    //     } while (pageToken != null);
    //
    //     Console.WriteLine($"Gevonden: {matched}, verwijderd: {deleted}");
    //     return deleted;
    // }
    //
    // private static async Task DeleteWithRetryAsync(CalendarService service, string calendarId, string eventId)
    // {
    //     for (int attempt = 0; attempt < 5; attempt++)
    //     {
    //         try
    //         {
    //             await service.Events.Delete(calendarId, eventId).ExecuteAsync();
    //             await Task.Delay(100); // ruim onder de rate limits blijven
    //             return;
    //         }
    //         catch (Google.GoogleApiException ex) when (
    //             ex.HttpStatusCode == System.Net.HttpStatusCode.Gone ||
    //             ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
    //         {
    //             return; // bestond al niet meer
    //         }
    //         catch (Google.GoogleApiException ex) when (
    //             ex.HttpStatusCode == (System.Net.HttpStatusCode)429 ||
    //             ex.HttpStatusCode == System.Net.HttpStatusCode.Forbidden) // rateLimitExceeded
    //         {
    //             await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)));
    //         }
    //     }
    //     throw new Exception($"Verwijderen van {eventId} mislukt na meerdere pogingen");
    // }
    
    #region Helper Methods

    private async Task<GoogleCredential> CreateCredentials(CancellationToken cancellationToken)
    {
        string[] scopes =
        {
            CalendarService.Scope.Calendar,
            CalendarService.Scope.CalendarEvents
        };
        
        using (var stream = new FileStream("/home/tyronlsg/RiderProjects/AgendaSyncer/agendasync-474013-865aff3f972f.json", FileMode.Open, FileAccess.Read))
        {
            ServiceAccountCredential serviceAccount = await CredentialFactory.FromStreamAsync<ServiceAccountCredential>(stream, cancellationToken);
            GoogleCredential credentials = serviceAccount.ToGoogleCredential().CreateScoped(scopes);
            return credentials;
        }
    }

    #endregion
}