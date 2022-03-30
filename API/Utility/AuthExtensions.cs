using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace API.Utility
{
    public static class AuthExtensions
    {

        private static void SetJwtOptions(JwtBearerOptions options, Config.OAuth2Config oauthConfig)
        {
            options.Authority = oauthConfig.Authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
            options.RequireHttpsMetadata = false;
        }

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

        public static void AddMyAuth(this IServiceCollection services, Config.OAuth2Config oauthConfig)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => SetJwtOptions(options, oauthConfig));

            services.ConfigureSwaggerGen(options =>
            {
                options.AddSecurityDefinition("OAuth2", GetSwaggerSecurityDefinition(oauthConfig));
            });
        }


        public static IApplicationBuilder UseMyAuth(this IApplicationBuilder app)
        {
            return app
                .UseAuthentication()
                .UseAuthorization();
        }

    }
}
