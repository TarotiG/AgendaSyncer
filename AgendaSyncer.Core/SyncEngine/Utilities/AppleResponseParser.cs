using System.Xml;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class AppleResponseParser
{
    public static CalendarEvent DeserializeToAppleEvent(string calendar)
    {
        CalendarCollection calendarCollection = CalendarCollection.Load(calendar);

        foreach (var icalendar in calendarCollection)
        {
            foreach (CalendarEvent calendarEvent in icalendar.Events)
            {
                return calendarEvent;
            }
        }
        
        return new CalendarEvent();
    }
    
    public static IEnumerable<string> ParseToCalendars(string response)
    {
        string[] calendars = ReadResponse(response).Split("BEGIN:VCALENDAR");
        foreach (string calendarString in calendars)
        {
            int indexEnd = calendarString.IndexOf("END:VCALENDAR", StringComparison.Ordinal);
            if (indexEnd != -1)
            {
                yield return $"BEGIN:VCALENDAR{calendarString.Substring(0, indexEnd)}END:VCALENDAR";
            }
        }
    }
    
    private static string ReadResponse(string response)
    {
        return ParseResponse(response).DocumentElement!.InnerText;
    }
    
    private static XmlDocument ParseResponse(string xml)
    {
        XmlDocument doc = new();
        doc.LoadXml(xml);
        return doc;
    }
}