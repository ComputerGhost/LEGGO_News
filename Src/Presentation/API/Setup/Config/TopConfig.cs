using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Setup.Config;

internal class TopConfig
{
    public SwaggerConfig Swagger { get; set; } = null!;
    public AuthenticationConfig Authentication { get; set; } = null!;
}
