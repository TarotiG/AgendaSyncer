using AgendaSyncer.Core.Exceptions;
using AgendaSyncer.Core.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace AgendaSyncer.Core.Services;

public class GoogleCalendarService : IGoogleCalendarService
{
    public CalendarService CreateConnection()
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