using System;

namespace Oauth.Wrappers.Resources
{
    public class Attachment
    {
        public string ContentType { get; set; }
        public bool IsInline { get; set; }
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}