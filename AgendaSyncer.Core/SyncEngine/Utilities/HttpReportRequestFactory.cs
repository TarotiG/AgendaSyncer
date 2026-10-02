using System.Globalization;
using System.Net.Http.Headers;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class HttpReportRequestFactory
{
    public static HttpReportRequest Create(
      string url,
      string user,
      string password,
      DateTimeOffset start,
      DateTimeOffset end)
    {
      var request = new HttpReportRequest(url, user, password, start, end);
      request.Headers.Add("Depth", "1");
      request.Headers.Add("Accept", "application/xml; charset=utf-8");
      return request;
    }
}

public class HttpReportRequest : HttpRequestMessage
{
    private static readonly HttpMethod Report = new HttpMethod("REPORT");
  
    
    internal HttpReportRequest(
      string url,
      string user,
      string password,
      DateTimeOffset start,
      DateTimeOffset end) : base(Report, url)
    {
      string startTime = start.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss'Z'", CultureInfo.InvariantCulture);
      string endTime = end.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss'Z'", CultureInfo.InvariantCulture);
      
      string payload = $"""
                                 <?xml version="1.0" encoding="UTF-8"?>
                                 <c:calendar-query xmlns:c="urn:ietf:params:xml:ns:caldav"
                                                   xmlns:d="DAV:">
                                   <d:prop>
                                     <c:calendar-data/>
                                   </d:prop>
                                   <c:filter>
                                     <c:comp-filter name="VCALENDAR">
                                       <c:comp-filter name="VEVENT">
                                         <c:time-range start="{startTime}" end="{endTime}"/>
                                       </c:comp-filter>
                                     </c:comp-filter>
                                   </c:filter>
                                 </c:calendar-query>
                                 """;
      
      Content = new StringContent(payload, Encoding.UTF8, "application/xml");
      Headers.Authorization = CreateBasicAuthenticationHeaderValue(user, password);
    }
    
    private static AuthenticationHeaderValue CreateBasicAuthenticationHeaderValue(string user, string password)
    {
      string encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
      return new AuthenticationHeaderValue("Basic", encodedCredentials);
    }
}