using System.Collections.Generic;

namespace Oauth.Wrappers.Resources
{
    public class CalendarGroup
    {
        public string Name { get; set; }
        public string ChangeKey { get; set; }
        public string ClassId { get; set; }
        public string Id { get; set; }
        public List<Calendar> Calendars { get; set; }
    }
}
