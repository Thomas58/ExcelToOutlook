namespace Oauth.Wrappers.Resources
{
    public enum BodyType { Text, HTML }

    public class ItemBody
    {
        public BodyType ContentType { get; set; }
        public string Content { get; set; }
    }
}