using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace API.Setup
{
    public static class SwaggerExtensions
    {
        private const string SecurityDefinitionName = "OAuth2";

        public static void AddMySecurityDefinition(this SwaggerGenOptions options, Uri oauthBase)
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

        public static void AddMySecurityRequirement(this SwaggerGenOptions options)
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
