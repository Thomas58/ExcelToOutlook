using Oauth.Wrappers;
using Oauth.Wrappers.Resources;
using System;
using System.Text;

namespace ExcelToOutlook.Tools
{
    public class RequestBuilder
    {
        public Request Request { get; private set; }
        private RequestBuilder(Request Request)
        {
            this.Request = Request;
        }
        
        #region Event Requests
        public static RequestBuilder GetCalendarView(DateTimeOffset start, DateTimeOffset end, string calendar_id = null)
        {
            Request Request = calendar_id == null ? new Request("/calendarview", Method2.GET) : new Request("/calendars/" + calendar_id + "/calendarview", Method2.GET);
            Request.AddParameter("startDateTime", start);
            Request.AddParameter("endDateTime", end);
            Request = EventParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder GetEvents(string calendar_id = null)
        {
            Request Request = calendar_id == null ? new Request("/events", Method2.GET) : new Request("/calendars/" + calendar_id + "/events", Method2.GET);
            Request = EventParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder GetEventInstances(string event_id, DateTimeOffset start, DateTimeOffset end)
        {
            Request Request = new Request("/events/" + event_id + "/instances", Method2.GET);
            Request.AddParameter("startDateTime", start);
            Request.AddParameter("endDateTime", end);
            Request = EventParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder GetEvent(string event_id)
        {
            Request Request = new Request("/events/" + event_id, Method2.GET);
            Request = EventParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder CreateEvent(Event calendarEvent, string calendar_id = null)
        {
            Request Request = calendar_id == null ? new Request("/events", Method2.POST) : new Request("/calendars/" + calendar_id + "/events", Method2.POST);
            Request.AddHeader("Content-Type", "application/json");
            Request = EventParameters(Request);
            Request.AddBody(calendarEvent);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder UpdateEvent(Event calendarEvent, string event_id)
        {
            Request Request = new Request("/events/" + event_id, Method2.PATCH);
            Request.AddHeader("Content-Type", "application/json");
            Request = EventParameters(Request);
            Request.AddBody(calendarEvent);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder DeleteEvent(string event_id) =>
            new RequestBuilder(new Request("/events/" + event_id, Method2.DELETE));

        public static RequestBuilder CountEvents(string calendar_id = null) =>
            new RequestBuilder(Count(calendar_id == null ? new Request("/events", Method2.GET) : new Request("/calendars/" + calendar_id + "/events", Method2.GET)));

        public static RequestBuilder CountEventInstances(string event_id) =>
            new RequestBuilder(Count(new Request("/events/" + event_id + "/instances", Method2.GET)));
        #endregion
        #region Calendar Requests
        public static RequestBuilder GetCalendars(string calendar_group_id = null)
        {
            Request Request = calendar_group_id == null ? new Request("/calendars", Method2.GET) : new Request("/calendargroups/" + calendar_group_id + "/calendars", Method2.GET);
            Request = CalendarParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder GetCalendar(string calendar_id = null)
        {
            Request Request = calendar_id == null ? new Request("/calendar", Method2.GET) : new Request("/calendars/" + calendar_id, Method2.GET);
            Request = CalendarParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder CreateCalendar(Calendar calendar, string calendar_group_id = null)
        {
            Request Request = calendar_group_id == null ? new Request("/calendars", Method2.POST) : new Request("/calendargroups/" + calendar_group_id + "/calendars", Method2.POST);
            Request.AddHeader("Content-Type", "application/json");
            Request = CalendarParameters(Request);
            Request.AddBody(calendar);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder UpdateCalendar(Calendar calendar, string calendar_id)
        {
            Request Request = new Request("/calendars/" + calendar_id, Method2.PATCH);
            Request.AddHeader("Content-Type", "application/json");
            Request = CalendarParameters(Request);
            Request.AddBody(calendar);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder DeleteCalendar(string calendar_id) =>
            new RequestBuilder(new Request("/calendars/" + calendar_id, Method2.DELETE));

        public static RequestBuilder CountCalendars(string calendar_group_id = null) =>
            new RequestBuilder(Count(calendar_group_id == null ? new Request("/calendars", Method2.GET) : new Request("/calendargroups/" + calendar_group_id + "/calendars", Method2.GET)));
        #endregion
        #region CalendarGroup Requests
        public static RequestBuilder GetCalendarGroups()
        {
            Request Request = new Request("/calendargroups", Method2.GET);
            Request = CalendarGroupParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder GetCalendarGroup(string calendar_group_id)
        {
            Request Request = new Request("/calendargroups/" + calendar_group_id, Method2.GET);
            Request = CalendarGroupParameters(Request);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder CreateCalendarGroup(CalendarGroup calendarGroup)
        {
            Request Request = new Request("/calendargroups", Method2.GET);
            Request = CalendarGroupParameters(Request);
            Request.AddBody(calendarGroup);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder UpdateCalendarGroup(CalendarGroup calendarGroup, string calendar_group_id)
        {
            Request Request = new Request("/calendargroups/" + calendar_group_id, Method2.PATCH);
            Request = CalendarGroupParameters(Request);
            Request.AddBody(calendarGroup);
            return new RequestBuilder(Request);
        }

        public static RequestBuilder DeleteCalendarGroup(string calendar_group_id) =>
            new RequestBuilder(new Request("/calendargroups/" + calendar_group_id, Method2.DELETE));

        public static RequestBuilder CountCalendarGroups() =>
            new RequestBuilder(Count(new Request("/calendargroups", Method2.GET)));
        #endregion

        public RequestBuilder Top(int no)
        {
            Request.AddParameter("$top", no);
            return this;
        }
        public RequestBuilder Skip(int no)
        {
            Request.AddParameter("$skip", no);
            return this;
        }

        public RequestBuilder Filter(string boolExpression)
        {
            StringBuilder StrBuilder = new StringBuilder(boolExpression);
            StrBuilder.Replace("&&", "and");
            StrBuilder.Replace("||", "or");
            StrBuilder.Replace("==", "eq");
            StrBuilder.Replace("!=", "ne");
            StrBuilder.Replace(">", "gt");
            StrBuilder.Replace(">=", "ge");
            StrBuilder.Replace("<", "lt");
            StrBuilder.Replace("<=", "le");
            Request.AddParameter("$filter", StrBuilder.ToString());
            return this;
        }

        public RequestBuilder OrderBy(string orderby) => OrderBy(new[] { orderby });
        public RequestBuilder OrderBy(string[] orderby)
        {
            if (0 < orderby.Length)
            {
                StringBuilder StrBuilder = new StringBuilder();
                StrBuilder.Append(orderby[0]);
                for (int i = 1; i < orderby.Length; i++)
                    StrBuilder.Append(',' + orderby[i]);
                Request.AddParameter("$orderby", StrBuilder.ToString());
            }
            return this;
        }

        #region Helper Methods
        private static Request Count(Request Request)
        {
            Request.Ressource = Request.Ressource + "/$count";
            return Request;
        }

        private static Request EventParameters(Request Request)
        {
            Request = Preference(Request);
            Request.AddParameter("$select", "Id,Subject,Start,End,IsAllDay,Body,BodyPreview,ChangeKey");
            return Request;
        }

        private static Request CalendarParameters(Request Request)
        {
            Request = Preference(Request);
            Request.AddParameter("$select", "Id,Name,Color,ChangeKey");
            return Request;
        }

        private static Request CalendarGroupParameters(Request Request)
        {
            Request = Preference(Request);
            Request.AddParameter("$select", "Id,Name,ClassId,ChangeKey");
            return Request;
        }

        private static Request Preference(Request Request)
        {
            Request.AddHeader("Prefer", "Europe/Berlin");
            return Request;
        }
        #endregion

        #region OData query parameters
        //$search to search for specific criteria
        //$filter to filter for specific criteria
        //$select to request specific properties
        //$orderby to sort results
        //$top and $skip to page results
        //$expand to expand message attachments and event attachments
        //$count to get the count of entities in a collection. This parameter goes in the URL path: .../me/calendars/$count
        #endregion
    }
}
