using API.Setup.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Setup;

internal static class AuthenticationSetup
{
    public static void AddMyAuthentication(this IServiceCollection services, TopConfig config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMyJwtBearer(config.Authentication);
    }

    private static void AddMyJwtBearer(this AuthenticationBuilder authentication, AuthenticationConfig config)
    {
        authentication.AddJwtBearer(options => new JwtBearerOptions
        {
            Authority = config.Authority,
            Audience = config.Audience,
            RequireHttpsMetadata = config.RequireHttpsMetadata,
        });
    }
}
