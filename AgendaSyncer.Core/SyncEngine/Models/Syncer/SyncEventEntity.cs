using AgendaSyncer.Core.SyncEngine.Enums;
using Google.Apis.Calendar.v3.Data;
using Ical.Net.DataTypes;

namespace AgendaSyncer.Core.SyncEngine.Models;

public class SyncEventEntity
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
                            string calUID,
                            List<Attachment> attachments)
    {
    }
    
    // public void getVEventSummary(VEvent event) 
    // {
    //     this.title = event.getSummary().getValue();
    // }

    // public void getVEventDescription(VEvent event) {
    //     this.description = event.getDescription() != null
    //         ? event.getDescription().getValue()
    //         : null;
    // }
    //
    // public void getVEventICalUID(VEvent event) {
    //     this.iCalUID = event.getUid().getValue();
    // }
    //
    // public void getVEventCreated(VEvent event) {
    //     // getCreated() kan null zijn — niet alle CalDAV clients zetten de CREATED property
    //     if (event.getCreated() != null && event.getCreated().getDate() != null) {
    //         this.created = new DateTime(event.getCreated().getDate());
    //     } else {
    //         // Fallback: gebruik huidige tijd als aanmaakdatum niet beschikbaar is
    //         this.created = new DateTime(System.currentTimeMillis());
    //     }
    // }
    //
    // public void getVEventStart(VEvent event) {
    //     String isoStartDate = event.getStartDate().getValue();
    //
    //     if (DateTimeMapper.isAllDay(isoStartDate)) {
    //         // All-day event: Google verwacht "yyyy-MM-dd" via setDate(), niet setDateTime()
    //         String googleDate = DateTimeMapper.convertICalDateToGoogleDate(isoStartDate);
    //         this.startDateTime = new EventDateTime()
    //                 .setDate(new com.google.api.client.util.DateTime(googleDate));
    //     } else {
    //         DateTime startDate = DateTimeMapper.convertICalDateTimeToGoogleDateTime(isoStartDate, "Europe/Amsterdam");
    //         this.startDateTime = new EventDateTime()
    //                 .setDateTime(startDate)
    //                 .setTimeZone("Europe/Amsterdam");
    //     }
    // }
    //
    // public void getVEventEnd(VEvent event) {
    //     String isoEndDate = event.getEndDate().getValue();
    //
    //     if (DateTimeMapper.isAllDay(isoEndDate)) {
    //         // All-day event: Google verwacht "yyyy-MM-dd" via setDate(), niet setDateTime()
    //         String googleDate = DateTimeMapper.convertICalDateToGoogleDate(isoEndDate);
    //         this.endDateTime = new EventDateTime()
    //                 .setDate(new com.google.api.client.util.DateTime(googleDate));
    //     } else {
    //         DateTime endDate = DateTimeMapper.convertICalDateTimeToGoogleDateTime(isoEndDate, "Europe/Amsterdam");
    //         this.endDateTime = new EventDateTime()
    //                 .setDateTime(endDate)
    //                 .setTimeZone("Europe/Amsterdam");
    //     }
    // }
    //
    // public void getVEventLocation(VEvent event) {
    //     this.location = event.getLocation() != null
    //             ? event.getLocation().getValue()
    //             : null;
    // }
    //
    // public void setEventOrigin(String calendar) {
    //     this.eventOrigin = calendar.equals("google")
    //             ? CalendarType.GOOGLE
    //             : CalendarType.APPLE;
    // }
    //
    // public CalendarType getEventOrigin() {
    //     return this.eventOrigin;
    // }
    //
    // public DateTime setCreatedToNow() {
    //     LocalDate date = LocalDate.now();
    //
    //     long milliSeconds = date.atStartOfDay(ZoneId.systemDefault()).toInstant().toEpochMilli();
    //     return new DateTime(milliSeconds);
    // }
    //
    // public void setICalUID() {
    //     if(this.iCalUID == null || this.iCalUID.equals("")) {
    //         this.iCalUID = UUID.randomUUID().toString();
    //     }
    // }
}