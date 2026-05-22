using AgendaSyncer.Core.SyncEngine.Enums;
using Google.Apis.Calendar.v3.Data;
using Ical.Net.DataTypes;

namespace AgendaSyncer.Core.SyncEngine.Models;

public record SyncEventDto
{
     public string id { get; init; }
     public string syncId { get; init; }
     public string title { get; init; }
     public string description { get; init; }
     public string location { get; init; }
     public EventDateTime startDateTime { get; init; }
     public EventDateTime endDateTime { get; init; }
     public DateTime timeZone { get; init; }
     public string recurrence { get; init; }
     public string organizerEmail { get; init; }
     public List<Attendee> attendees { get; init; }
     // public Visibility visibility { get; init; }
     public string sequence { get; init; }
     public DateTime created { get; init; }
     public DateTime updated { get; init; }
     // public Status status { get; init; }
     public string iCalUID { get; init; }
     public List<Attachment> attachments { get; init; }
     private CalendarType eventOrigin { get; init; }
}