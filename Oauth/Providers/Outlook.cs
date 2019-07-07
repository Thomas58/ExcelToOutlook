using System;

namespace Oauth.Providers
{
    public class Outlook : Oauth2Provider
    {
        private string host = "https://outlook.office.com/api/v2.0/me";
        private string clientId = "Client_Id";
        private string clientSecret = "Client_Secret";
        private string redirectPath = "/test/";
        private string redirectUri = "http://localhost:8081"; //TODO: Change Redirect_URI.
        private string autherizeEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
        private string tokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
        private string scope = "https://outlook.office.com/calendars.readwrite";

        public override string Host                 { get { return host;                } }
        public override string ClientId             { get { return clientId;            } }
        public override string ClientSecret         { get { return clientSecret;        } }
        public override string RedirectPath         { get { return redirectPath;        } }
        public override string RedirectUri          { get { return redirectUri;         } }
        public override string AutherizeEndpoint    { get { return autherizeEndpoint;   } }
        public override string TokenEndpoint        { get { return tokenEndpoint;       } }
        public override string Scope                { get { return scope;               } }

        public override Uri AuthorizationWebLink
        {
            get
            {
                var uri = new UriBuilder(AutherizeEndpoint);
                uri.Query =
                    "client_id=" + ClientId + "&" +
                    "redirect_uri=" + RedirectUri + RedirectPath + "&" +
                    "response_type=code&" +
                    "scope=" + Scope;
                return uri.Uri;
            }
        }
    }
}
