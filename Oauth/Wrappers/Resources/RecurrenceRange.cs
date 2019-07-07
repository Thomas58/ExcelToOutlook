using System;

namespace Oauth.Wrappers.Resources
{
    public enum RecurrenceRangeType { EndDate, NoEnd, Numbered }

    public class RecurrenceRange
    {
        public RecurrenceRangeType Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int NumberOfOccurrences { get; set; }
    }
}