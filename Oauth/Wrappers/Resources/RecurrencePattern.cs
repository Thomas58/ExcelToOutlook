using System.Collections.Generic;

namespace Oauth.Wrappers.Resources
{
    public enum RecurrencePatternType { Daily, Weekly, AbsoluteMonthly, RelativeMonthly, AbsoluteYearly, RelativeYearly }
    public enum DaysOfWeek { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }

    public class RecurrencePattern
    {
        public RecurrencePatternType Type { get; set; }
        public int Interval { get; set; }
        public int DayOfMonth { get; set; }
        public int Month { get; set; }
        public List<DaysOfWeek> DaysOfWeek { get; set; }
    }
}