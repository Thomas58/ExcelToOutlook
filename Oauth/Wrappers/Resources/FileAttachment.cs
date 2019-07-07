using System;

namespace Oauth.Wrappers.Resources
{
    public class FileAttachment : Attachment
    {
        public byte[] ContentBytes { get; set; }
        public string ContentId { get; set; }
        public string ContentLocation { get; set; }
    }
}