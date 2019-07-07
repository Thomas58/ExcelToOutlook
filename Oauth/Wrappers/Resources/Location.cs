namespace Oauth.Wrappers.Resources
{
    public class Location
    {
        public string DisplayName { get; set; }
        public PhysicalAddress Address { get; set; }
        public GeoCoordinates Coordinates { get; set; }
        public string LocationEmailAddress { get; set; }
    }
}