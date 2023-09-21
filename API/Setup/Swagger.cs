using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Setup
{
    internal static class Swagger
    {
        private const string SecurityDefinitionName = "OAuth2";

        public static void AddMySwagger(this IServiceCollection services, Config config)
        {
            services.AddSwaggerGen(options =>
            {
                //options.AddMySecurityDefinition(new Uri(config.JwtSettings.Authority!));
                //options.AddMySecurityRequirement();
            });
        }

        private static void AddMySecurityDefinition(this SwaggerGenOptions options, Uri oauthBase)
        {
            options.AddSecurityDefinition(SecurityDefinitionName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(oauthBase, "/oauth2/authorize"),
                        TokenUrl = new Uri(oauthBase, "/oauth2/token"),
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
                            Type = ReferenceType.SecurityScheme,
                            Id = SecurityDefinitionName
                        }
                    },
                    new string[] { }
                }
            });
        }
    }
}
