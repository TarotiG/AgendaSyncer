using System.Net;
using System.Net.Http;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpHandler
{
    public static async Task<string> SendPropFindRequest(HttpClient httpClient, string url)
    {
        HttpPropfindRequest request = HttpPropfindRequestFactory.Create(url);
        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> SendReportRequest(HttpClient httpClient, string url)
    {
        HttpReportRequest request = HttpReportRequestFactory.Create(url);
        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
}