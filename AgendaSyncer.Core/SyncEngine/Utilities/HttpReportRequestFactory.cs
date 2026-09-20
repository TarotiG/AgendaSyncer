namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpReportRequestFactory
{
    public static HttpReportRequest Create(string url)
    {
      var request = new HttpReportRequest(url);
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
    
    internal HttpReportRequest(string url) : base(Report, url)
    {
      Content = new StringContent(_payload, Encoding.UTF8, "application/xml");
    }
}