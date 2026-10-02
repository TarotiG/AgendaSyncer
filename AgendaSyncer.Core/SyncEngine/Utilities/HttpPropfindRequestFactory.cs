using System.Net.Http.Headers;
using DotNetEnv;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpPropfindRequestFactory
{
    public static HttpPropfindRequest Create(string url, string user, string password)
    {
        var request = new HttpPropfindRequest(url, user, password);
        
        request.Headers.Add("Accept", "application/xml; charset=utf-8");
        request.Headers.Add("Depth", "1");
        return request;
    }
}

public class HttpPropfindRequest : HttpRequestMessage
{
    private static readonly HttpMethod Propfind = new HttpMethod("PROPFIND");
    private string _payload = """
                             <?xml version="1.0" encoding="UTF-8"?>
                             <d:propfind xmlns:d="DAV:" xmlns:cs="http://calendarserver.org/ns/">
                               <d:prop>
                                 <d:displayname/>
                                 <d:resourcetype/>
                                 <d:current-user-privilege-set/>
                               </d:prop>
                             </d:propfind> 
                             """;
    
    
    internal HttpPropfindRequest(string url, string user, string password) : base(Propfind, url)
    {
        Content = new StringContent(_payload, Encoding.UTF8, "application/xml");
        Headers.Authorization = CreateBasicAuthenticationHeaderValue(user, password);
    }

    private static AuthenticationHeaderValue CreateBasicAuthenticationHeaderValue(string user, string password)
    {
        string encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
        return new AuthenticationHeaderValue("Basic", encodedCredentials);
    }

}