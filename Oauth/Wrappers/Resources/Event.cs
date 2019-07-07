using System;
using System.Collections.Generic;

namespace Oauth.Wrappers.Resources
{
    public enum EventType { SingleInstance, Occurrence, Exception, SeriesMaster }
    public enum FreeBusyStatus { Busy, Free, Oof, Tentative, Unknown, WorkingElsewhere }
    public enum Importance { Low, Normal, High }
    public enum Sensitivity { Normal, Personal, Private, Confidential }

    public class Event
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public DateTimeTimeZone Start { get; set; }
        public DateTimeTimeZone End { get; set; }
        public bool IsAllDay { get; set; }
        public ItemBody Body { get; set; }
        public string BodyPreview { get; private set; }
        public string ChangeKey { get; set; }

        //public List<Attachment> Attachments { get; set; }
        //public List<Attendee> Attendees { get; set; }
        //public Calendar Calendar { get; set; }
        //public List<string> Categories { get; set; }
        //public DateTimeOffset CreatedDateTime { get; set; }
        //public DateTimeOffset LastModifiedDateTime { get; set; }
        //public List<Extension> Extensions { get; set; }
        //public bool HasAttachments { get; set; }
        //public Importance Importance { get; set; }
        //public bool IsCancelled { get; set; }
        //public bool IsOrganizer { get; set; }
        //public bool IsReminderOn { get; set; }
        //public Location Location { get; set; }
        //public string OnlineMeetingUrl { get; set; }
        //public Recipient Organizer { get; set; }
        //public string OriginalEndTimeZone { get; set; }
        //public string OriginalStartTimeZone { get; set; }
        //public PatternedRecurrence Recurrence { get; set; }
        //public int ReminderMinutesBeforeStart { get; set; }
        //public bool ResponseRequested { get; set; }
        //public ResponseStatus ResponseStatus { get; set; }
        //public Sensitivity Sensitivity { get; set; }
        //public string SeriesMasterId { get; set; }
        //public FreeBusyStatus ShowAs { get; set; }
        //public EventType Type { get; set; }
        //public string WebLink { get; set; }
    }
}