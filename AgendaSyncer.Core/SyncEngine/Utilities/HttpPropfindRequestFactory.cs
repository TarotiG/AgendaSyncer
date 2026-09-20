using DotNetEnv;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpPropfindRequestFactory
{
    public static HttpPropfindRequest Create(string url, string user, string password)
    {
        var request = new HttpPropfindRequest(url);
        request.SetAuthentication(user, password);
        
        request.Headers.Add("Depth", "0");
        request.Headers.Add("Authorization", request.Authentication);
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
                             """;
    
    internal string Authentication;
    
    
    internal HttpPropfindRequest(string url) : base(Propfind, url)
    {
        Content = new StringContent(_payload, Encoding.UTF8, "application/xml");
    }

    internal void SetAuthentication(string user, string password)
    {
        Authentication = $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"))}";
    }

}