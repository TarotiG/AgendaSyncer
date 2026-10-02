using System.Net.Http.Headers;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpReportRequestFactory
{
    public static HttpReportRequest Create(string url, string user, string password)
    {
      var request = new HttpReportRequest(url, user, password);
      request.Headers.Add("Depth", "1");
      return request;
    }
}

public class HttpReportRequest : HttpRequestMessage
{
    private static readonly HttpMethod Report = new HttpMethod("REPORT");
    private string _payload = """
                              <?xml version="1.0" encoding="UTF-8"?>
                              <c:calendar-query xmlns:c="urn:ietf:params:xml:ns:caldav"
                                                xmlns:d="DAV:">
                                <d:prop>
                                  <c:calendar-data/>
                                </d:prop>
                                <c:filter>
                                  <c:comp-filter name="VCALENDAR">
                                    <c:comp-filter name="VEVENT">
                                      <c:time-range start="%s" end="%s"/>
                                    </c:comp-filter>
                                  </c:comp-filter>
                                </c:filter>
                              </c:calendar-query>
                              """;
    
    internal HttpReportRequest(string url, string user, string password) : base(Report, url)
    {
      Content = new StringContent(_payload, Encoding.UTF8, "application/xml");
      Headers.Authorization = CreateBasicAuthenticationHeaderValue("username", "password");
    }
    
    private static AuthenticationHeaderValue CreateBasicAuthenticationHeaderValue(string user, string password)
    {
      string encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
      return new AuthenticationHeaderValue("Basic", encodedCredentials);
    }
}