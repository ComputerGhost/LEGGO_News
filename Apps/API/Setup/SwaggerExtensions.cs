using API.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace API.Setup
{
    public static class SwaggerExtensions
    {

        private static OpenApiSecurityScheme GetSwaggerSecurityDefinition(Config.OAuth2Config oauthConfig)
        {
            var oauthBase = new Uri(oauthConfig.Authority);

            return new OpenApiSecurityScheme
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
                            { "Public", "public" }
                        }
                    }
                }
            };
        }

        private static OpenApiSecurityRequirement GetSwaggerSecurityRequirement()
        {
            return new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "OAuth2"
                        }
                    },
                    new string[] { }
                }
            };
        }

        public static void AddMySwagger(this IServiceCollection services, Config.OAuth2Config oauthConfig)
        {
            services.AddSwaggerGen(options =>
            {
                options.DocumentFilter<JsonPatchDocumentFilter>();
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml"));
                options.AddSecurityDefinition("OAuth2", GetSwaggerSecurityDefinition(oauthConfig));
                options.AddSecurityRequirement(GetSwaggerSecurityRequirement());
            });
        }


        public static void UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

    }
}
