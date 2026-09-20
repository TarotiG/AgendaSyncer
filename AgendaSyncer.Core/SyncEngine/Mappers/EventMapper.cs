using AgendaSyncer.Core.SyncEngine.Models;
using AgendaSyncer.Core.SyncEngine.Models.Syncer;

namespace AgendaSyncer.Core.SyncEngine.Mappers;

public static class EventMapper
{
    #region Google EventMapper

    public static SyncEventDto MapGoogleEventToSyncEvent(Event googleEvent)
    {
        SyncEventEntity syncEvent = new SyncEventEntity
        {
            Id = googleEvent.Id,
            Title = googleEvent.Summary,
            Description = googleEvent.Description,
            Created = googleEvent.CreatedDateTimeOffset,
            Updated = googleEvent.UpdatedDateTimeOffset,
            StartDateTime = googleEvent.Start,
            EndDateTime = googleEvent.End,
            Location = googleEvent.Location,
            ICalUID = googleEvent.ICalUID,
            // Attachments = googleEvent.Attachments
        };


        return syncEvent.ToSyncEventDto();
    }

//     public static List<SyncEventDto> mapGoogleEventsToSyncEventDto(List<Event> googleEvents)
//     {
//         ArrayList<SyncEventDto> syncEventDtoList = new ArrayList<>();
//
//         for(Event event : googleEvents) {
//             SyncEventDto syncEventDto = new SyncEventDto();
//             syncEventDto.id = event.getId();
//             syncEventDto.title = event.getSummary();
// //            syncEventDto.description = event.getDescription();
//             syncEventDto.created = event.getCreated();
//             syncEventDto.updated = event.getUpdated();
//             syncEventDto.startDateTime = event.getStart();
//             syncEventDto.endDateTime = event.getEnd();
//             syncEventDto.location = event.getLocation();
//             syncEventDto.iCalUID = event.getICalUID();
//             
//             // Extract or generate syncId for duplicate prevention
//             if (event.getExtendedProperties() != null && 
//                 event.getExtendedProperties().getPrivate() != null &&
//                 event.getExtendedProperties().getPrivate().containsKey("syncId")) {
//                 syncEventDto.syncId = event.getExtendedProperties().getPrivate().get("syncId");
//             } else {
//                 syncEventDto.syncId = "google_" + event.getId();
//             }
//             
//             syncEventDto.setEventOrigin("google");
// //            syncEventDto.organizerEmail = event.getOrganizer();
//
//             syncEventDtoList.add(syncEventDto);
//         }
//
//         return syncEventDtoList;
//     }
//
//     public static List<Event> mapSyncEventsDtoBackToGoogleEvents(List<SyncEventDto> syncEventDtoList) {
//         ArrayList<Event> googleEvents = new ArrayList<>();
//
//         for (SyncEventDto event : syncEventDtoList) {
//             Event googleEvent = EventMapper.mapSyncEventDtoBackToGoogleEvent(event);
//             googleEvents.add(googleEvent);
//         }
//
//         return googleEvents;
//     }
//
//     public static Event mapSyncEventDtoBackToGoogleEvent(SyncEventDto event) {
//         Event googleEvent = new Event();
//
//         googleEvent.setSummary(event.title);
//         googleEvent.setStart(event.startDateTime);
//         googleEvent.setEnd(event.endDateTime);
//         
//         // Set syncId in extended properties for duplicate prevention
//         if (event.syncId != null) {
//             Map<String, String> privateProperties = new HashMap<>();
//             privateProperties.put("syncId", event.syncId);
//             com.google.api.services.calendar.model.Event.ExtendedProperties extendedProps = 
//                 new com.google.api.services.calendar.model.Event.ExtendedProperties();
//             extendedProps.setPrivate(privateProperties);
//             googleEvent.setExtendedProperties(extendedProps);
//         }
//
//         return googleEvent;
//     }
//
//     public static List<DateTime> createGoogleDateTimeForEvent(String startDate, String endDate) {
//         return Arrays.asList(
//                 new DateTime(startDate),
//                 new DateTime(endDate)
//         );
//     }
    #endregion
    
    #region Apple EventMapper
    //     public void getVEventSummary(VEvent event) {
    //         this.title = event.getSummary().getValue();
    //     }
    //
    //     public void getVEventDescription(VEvent event) {
    //         this.description = event.getDescription() != null
    //             ? event.getDescription().getValue()
    //             : null;
    //     }
    //
    //     public void getVEventICalUID(VEvent event) {
    //         this.iCalUID = event.getUid().getValue();
    //     }
    //
    //     public void getVEventCreated(VEvent event) {
    //         // getCreated() kan null zijn — niet alle CalDAV clients zetten de CREATED property
    //         if (event.getCreated() != null && event.getCreated().getDate() != null) {
    //             this.created = new DateTime(event.getCreated().getDate());
    //         } else {
    //             // Fallback: gebruik huidige tijd als aanmaakdatum niet beschikbaar is
    //             this.created = new DateTime(System.currentTimeMillis());
    //         }
    //     }
    //
    //     public void getVEventStart(VEvent event) {
    //         String isoStartDate = event.getStartDate().getValue();
    //
    //         if (DateTimeMapper.isAllDay(isoStartDate)) {
    //             // All-day event: Google verwacht "yyyy-MM-dd" via setDate(), niet setDateTime()
    //             String googleDate = DateTimeMapper.convertICalDateToGoogleDate(isoStartDate);
    //             this.startDateTime = new EventDateTime()
    //                     .setDate(new com.google.api.client.util.DateTime(googleDate));
    //         } else {
    //             DateTime startDate = DateTimeMapper.convertICalDateTimeToGoogleDateTime(isoStartDate, "Europe/Amsterdam");
    //             this.startDateTime = new EventDateTime()
    //                     .setDateTime(startDate)
    //                     .setTimeZone("Europe/Amsterdam");
    //         }
    //     }
    //
    //     public void getVEventEnd(VEvent event) {
    //         String isoEndDate = event.getEndDate().getValue();
    //
    //         if (DateTimeMapper.isAllDay(isoEndDate)) {
    //             // All-day event: Google verwacht "yyyy-MM-dd" via setDate(), niet setDateTime()
    //             String googleDate = DateTimeMapper.convertICalDateToGoogleDate(isoEndDate);
    //             this.endDateTime = new EventDateTime()
    //                     .setDate(new com.google.api.client.util.DateTime(googleDate));
    //         } else {
    //             DateTime endDate = DateTimeMapper.convertICalDateTimeToGoogleDateTime(isoEndDate, "Europe/Amsterdam");
    //             this.endDateTime = new EventDateTime()
    //                     .setDateTime(endDate)
    //                     .setTimeZone("Europe/Amsterdam");
    //         }
    //     }
    //
    //     public void getVEventLocation(VEvent event) {
    //         this.location = event.getLocation() != null
    //                 ? event.getLocation().getValue()
    //                 : null;
    //     }
    //
    //     public void setEventOrigin(String calendar) {
    //         this.eventOrigin = calendar.equals("google")
    //                 ? CalendarType.GOOGLE
    //                 : CalendarType.APPLE;
    //     }
    //
    //     public CalendarType getEventOrigin() {
    //         return this.eventOrigin;
    //     }
    //
    //     public DateTime setCreatedToNow() {
    //         LocalDate date = LocalDate.now();
    //
    //         long milliSeconds = date.atStartOfDay(ZoneId.systemDefault()).toInstant().toEpochMilli();
    //         return new DateTime(milliSeconds);
    //     }
    //
    //     public void setICalUID() {
    //         if(this.iCalUID == null || this.iCalUID.equals("")) {
    //             this.iCalUID = UUID.randomUUID().toString();
    //         }
    //     }
    #endregion
}