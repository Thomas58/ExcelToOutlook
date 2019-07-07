//using Oauth.Wrappers.Resources;

//namespace ExcelToOutlook.Tools
//{
//    public class WrapConverter
//    {
//        public static Event Convert(Model.Event @Event)
//        {
//            Event NewEvent = new Event() { Subject = Event.Subject };
//            if (Event.Body != null)
//                NewEvent.Body = new ItemBody()
//                {
//                    ContentType = BodyType.HTML,
//                    Content = Event.Body
//                };
//            if (Event.End != null)
//            {
//                NewEvent.Start = new DateTimeTimeZone()
//                {
//                    DateTime = Event.Start,
//                    TimeZone = Event.TimeZone
//                };
//                NewEvent.End = new DateTimeTimeZone()
//                {
//                    DateTime = Event.End,
//                    TimeZone = Event.TimeZone
//                };
//            }
//            else
//            {
//                NewEvent.Start = new DateTimeTimeZone()
//                {
//                    DateTime = Event.Start,
//                    TimeZone = Event.TimeZone
//                };
//                NewEvent.End = new DateTimeTimeZone()
//                {
//                    DateTime = Event.Start,
//                    TimeZone = Event.TimeZone
//                };
//            }
//            NewEvent.IsAllDay = Event.IsAllDay;
//            return NewEvent;
//        }

//        public static Model.Event Convert(Event @Event)
//        {
//            return new Model.Event()
//            {
//                Subject = Event.Subject,
//                Body = Event.Body.Content,
//                TimeZone = Event.Start.TimeZone,
//                Start = Event.Start.DateTime,
//                End = Event.End.DateTime,
//                IsAllDay = Event.IsAllDay
//            };
//        }

//        public static Calendar Convert(Model.Calendar Calendar)
//        {
//            return new Calendar()
//            {
//                Name = Calendar.Name,
//                Color = Model.ColorExt.ToString(Calendar.Color)
//            };
//        }

//        public static Model.Calendar Convert(Calendar Calendar)
//        {
//            return new Model.Calendar()
//            {
//                Name = Calendar.Name,
//                Color = Model.ColorExt.FromString(Calendar.Color)
//            };
//        }
//    }
//}
