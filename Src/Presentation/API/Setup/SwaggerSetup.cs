using API.Setup.Config;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Setup;

internal static class SwaggerSetup
{
    private const string SECURITY_DEFINITION_NAME = "OAuth2";

    public static void AddMySwagger(this IServiceCollection services, TopConfig config)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddMySecurityDefinition(config.Swagger);
            options.AddMySecurityRequirement();
        });
    }

    private static void AddMySecurityDefinition(this SwaggerGenOptions options, SwaggerConfig config)
    {
        options.AddSecurityDefinition(SECURITY_DEFINITION_NAME, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(config.AuthorizationUrl),
                    RefreshUrl = new Uri(config.RefreshUrl),
                    TokenUrl = new Uri(config.TokenUrl),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "Needed for sign-in." }
                    }
                }
            }
        });
    }

    private static void AddMySecurityRequirement(this SwaggerGenOptions options)
    {
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = SECURITY_DEFINITION_NAME,
                        Type = ReferenceType.SecurityScheme,
                    }
                },
                new string[] { }
            }
        });
    }
}
