using System.Net;
using System.Net.Http;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public class HttpHandler
{
    HttpClient _httpClient;
    
    public void CreateHttpClient()
    {
        _httpClient = new HttpClient();
    }

    private async Task<string> SendPropFindRequest(string url)
    {
        HttpPropfindRequest request = HttpPropfindRequestFactory.Create(url);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }

    private void SendReportRequest()
    {
    }
}