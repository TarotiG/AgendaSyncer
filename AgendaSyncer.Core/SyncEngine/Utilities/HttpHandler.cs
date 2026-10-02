// using System.Net;
// using System.Net.Http;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpHandler
{
    public static async Task<HttpResponseMessage> SendPropFindRequest(HttpClient httpClient, string url, string user, string password)
    {
        HttpPropfindRequest request = HttpPropfindRequestFactory.Create(url, user, password);
        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return response;
    }

    public static async Task<HttpResponseMessage> SendReportRequest(HttpClient httpClient, string url, string user, string password)
    {
        HttpReportRequest request = HttpReportRequestFactory.Create(url, user, password);
        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        return response;
    }
}