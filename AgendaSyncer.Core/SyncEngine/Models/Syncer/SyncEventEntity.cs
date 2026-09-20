using AgendaSyncer.Core.SyncEngine.Enums;
using Google.Apis.Calendar.v3.Data;
using Ical.Net.DataTypes;

namespace AgendaSyncer.Core.SyncEngine.Models.Syncer;

public class SyncEventEntity
{
    public string Id { get; set; }
    public string SyncId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public EventDateTime StartDateTime { get; set; }
    public EventDateTime EndDateTime { get; set; }
    public DateTime TimeZone { get; set; }
    public string Recurrence { get; set; }
    public string OrganizerEmail { get; set; }
    public List<Attendee> Attendees { get; set; }
    // public Visibility EventVisibility { get; set; }
    public string Sequence { get; set; }
    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    // public Status EventStatus { get; set; }
    public string ICalUID { get; set; }
    public List<Attachment>? Attachments { get; set; }
    private CalendarType EventOrigin { get; set; }
    
    public SyncEventEntity()
    {
    }
    
    public SyncEventEntity(string id,
                            string syncId,
                            string title,
                            string description,
                            string location,
                            EventDateTime startDateTime,
                            EventDateTime endDateTime,
                            DateTime timeZone,
                            string recurrence,
                            string organizerEmail,
                            List<Attendee> attendees,
                            DateTime created,
                            DateTime updated,
                            string icalUid,
                            List<Attachment> attachments)
    {
        Id = id;
        SyncId = syncId;
        Title = title;
        Description = description;
        Location = location;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        TimeZone = timeZone;
        Recurrence = recurrence;
        OrganizerEmail = organizerEmail;
        Attendees = attendees;
        Created = created;
        Updated = updated;
        ICalUID = icalUid;
        Attachments = attachments;
    }

    public SyncEventDto ToSyncEventDto()
    {
        SyncEventDto syncEventDto = new SyncEventDto
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Location = Location,
            StartDateTime = StartDateTime,
            EndDateTime = EndDateTime,
            TimeZone = TimeZone,
            Recurrence = Recurrence,
            OrganizerEmail = OrganizerEmail,
            Attendees = Attendees,
            ICalUID = ICalUID,
        };
        return syncEventDto;
    }
}