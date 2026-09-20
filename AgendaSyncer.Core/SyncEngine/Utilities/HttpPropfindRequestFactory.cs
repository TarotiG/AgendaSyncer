using System.Runtime.CompilerServices;
using System.Text;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpPropfindRequestFactory
{
    public static HttpPropfindRequest Create(string url)
    {
        var request = new HttpPropfindRequest(url);
        request.Headers.Add("Depth", "0");
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
    
    
    public HttpPropfindRequest(string url) : base(Propfind, url)
    {
        Content = new StringContent(_payload, Encoding.UTF8, "application/xml");
    }
    
}