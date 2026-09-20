namespace AgendaSyncer.Core.SyncEngine.Utilities;

public class HttpReportRequestFactory
{
    
}

// String body = """
//               <?xml version="1.0" encoding="UTF-8"?>
//               <c:calendar-query xmlns:c="urn:ietf:params:xml:ns:caldav"
//                                 xmlns:d="DAV:">
//                 <d:prop>
//                   <c:calendar-data/>
//                 </d:prop>
//                 <c:filter>
//                   <c:comp-filter name="VCALENDAR">
//                     <c:comp-filter name="VEVENT">
//                       <c:time-range start="%s" end="%s"/>
//                     </c:comp-filter>
//                   </c:comp-filter>
//                 </c:filter>
//               </c:calendar-query>
//               """.formatted(startFormatted, endFormatted);