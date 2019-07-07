using System.Timers;

namespace Oauth
{
    public class Oauth2AccessToken
    {
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; }

        public bool Expired { get; private set; } = false;
        private Timer ExpireTimer = null;

        public Oauth2AccessToken Init()
        {
            if (ExpireTimer == null)
            {
                ExpireTimer = new Timer(expires_in * 1000);
                ExpireTimer.AutoReset = false;
                ExpireTimer.Elapsed += new ElapsedEventHandler((source, e) => {
                    Expired = true;
                });
                ExpireTimer.Start();
            }
            return this;
        }
    }
}
