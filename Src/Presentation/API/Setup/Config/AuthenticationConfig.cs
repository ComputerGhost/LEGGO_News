namespace API.Setup.Config;

internal class AuthenticationConfig
{
    public string Authority { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public bool RequireHttpsMetadata { get; set; } = true;
}
