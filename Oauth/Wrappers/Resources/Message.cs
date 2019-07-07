using System;
using System.Collections.Generic;

namespace Oauth.Wrappers.Resources
{
    public enum MeetingMessageType { None, MeetingRequest, MeetingCancelled, MeetingAccepted, MeetingTentativelyAccepted, MeetingDeclined }
    public enum InferenceClassificationType { Focused, Other }

    public class Message
    {
        public List<Attachment> Attachments { get; set; }
        public List<Recipient> BccRecipients { get; set; }
        public ItemBody Body { get; set; }
        public string BodyPreview { get; set; }
        public List<string> Categories { get; set; }
        public List<Recipient> CcRecipients { get; set; }
        public string ChangeKey { get; set; }
        public string ConversationId { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        //public List<Extension> Extensions { get; set; }
        public Recipient From { get; set; }
        public bool HasAttachments { get; set; }
        public string Id { get; set; }
        public Importance Importance { get; set; }
        public InferenceClassificationType InferenceClassification { get; set; }
        public bool IsDeliveryReceiptRequested { get; set; }
        public bool IsDraft { get; set; }
        public bool IsRead { get; set; }
        public bool IsReadReceiptRequested { get; set; }
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public string ParentFolderId { get; set; }
        public DateTimeOffset ReceivedDateTime { get; set; }
        public List<Recipient> ReplyTo { get; set; }
        public Recipient Sender { get; set; }
        public DateTimeOffset SentDateTime { get; set; }
        public string Subject { get; set; }
        public List<Recipient> ToRecipients { get; set; }
        public ItemBody UniqueBody { get; set; }
        public string WebLink { get; set; }
    }
}