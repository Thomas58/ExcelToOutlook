using System;

namespace ExcelToOutlook.Model
{
    public class Event
    {
        public Event() { }

        public string Subject { get; set; } = "";
        public int Category { get; set; } = 0;
        public string Body { get; set; } = "";
        public DateTime Start { get; set; } = new DateTime();
        public DateTime End { get; set; } = new DateTime();
        public bool IsAllDay { get; set; } = true;

        public string StartDate { get { return Start.ToShortTimeString(); } }
        public string EndDate { get { return End.ToShortTimeString(); } }

        public Event Clone() => Clone(this);
        private Event Clone(Event old) => new Event()
        {
            Body = old.Body,
            End = old.End,
            IsAllDay = old.IsAllDay,
            Start = old.Start,
            Subject = old.Subject,
            Category = old.Category
        };
    }
}