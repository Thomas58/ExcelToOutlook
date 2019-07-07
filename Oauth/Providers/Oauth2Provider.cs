using System;

namespace Oauth
{
    public abstract class Oauth2Provider
    {
        public abstract string Host { get; }
        public abstract string ClientId { get; }
        public abstract string ClientSecret { get; }
        public abstract string RedirectPath { get; }
        public abstract string RedirectUri { get; }
        public abstract string AutherizeEndpoint { get; }
        public abstract string TokenEndpoint { get; }
        public abstract string Scope { get; }

        public abstract Uri AuthorizationWebLink { get; }
    }
}
