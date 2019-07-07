using System.Collections.Generic;

namespace Oauth.Wrappers.Resources
{
    public class Calendar
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ChangeKey { get; set; }
        public string Color { get; set; }
        public List<Event> CalendarView { get; set; }
        public List<Event> Events { get; set; }
    }
}
