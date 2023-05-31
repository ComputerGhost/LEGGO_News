namespace CMS.Setup
{
    public class Config
    {
        public class OAuthConfig
        {
            public string Authority { get; set; } = null!;
            public string ClientId { get; set; } = null!;
            public string ClientSecret { get; set; } = null!;
            public bool? RequireHttpsMetadata { get; set; }
        }

        public Uri ApiBaseUri { get; set; } = null!;

        public string ConnectionString { get; set; } = null!;

        public OAuthConfig OAuth { get; set; } = null!;
    }
}
