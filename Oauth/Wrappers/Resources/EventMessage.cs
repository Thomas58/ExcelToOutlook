namespace Oauth.Wrappers.Resources
{
    public class EventMessage : Message
    {
        public Event Event { get; set; }
        public MeetingMessageType MeetingMessageType { get; set; }
    }
}
