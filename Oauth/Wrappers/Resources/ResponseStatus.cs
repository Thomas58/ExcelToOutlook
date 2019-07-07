using System;

namespace Oauth.Wrappers.Resources
{
    public enum ResponseType { None, Organized, TentativelyAccepted, Accepted, Declined, NotResponded }

    public class ResponseStatus
    {
        public ResponseType Response { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}