namespace Oauth.Wrappers.Resources
{
    public class Attendee
    {
        public enum AttendeeType { Required, Optional, Resource }
        public ResponseStatus Status { get; set; }
        public AttendeeType Type { get; set; }
    }
}