namespace CMS
{
    public struct Config
    {
        public struct OIDCConfig
        {
            public string Authority { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string JwtCookieName { get; set; }
        }

        public string APIBaseUri { get; set; }

        public OIDCConfig OIDC { get; set; }
    }
}
