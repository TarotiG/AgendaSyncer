using AgendaSyncer.Core.SyncEngine.Enums;
using Google.Apis.Calendar.v3.Data;
using Ical.Net.DataTypes;

namespace AgendaSyncer.Core.SyncEngine.Models;

/// <summary>
/// Record om events te maken naar een calendar
/// </summary>
public record SyncEventDto()
{
     public string Id { get; init; }
     public string SyncId { get; init; }
     public string Title { get; init; }
     public string Description { get; init; }
     public string Location { get; init; }
     public EventDateTime StartDateTime { get; init; }
     public EventDateTime EndDateTime { get; init; }
     public DateTime TimeZone { get; init; }
     public string Recurrence { get; init; }
     public string OrganizerEmail { get; init; }
     public List<Attendee> Attendees { get; init; }
     // public Visibility Visibility { get; init; }
     public string Sequence { get; init; }
     public DateTime Created { get; init; }
     public DateTime Updated { get; init; }
     // public Status Status { get; init; }
     public string CalUID { get; init; }
     public List<Attachment> Attachments { get; init; }
     private CalendarType EventOrigin { get; init; }
}