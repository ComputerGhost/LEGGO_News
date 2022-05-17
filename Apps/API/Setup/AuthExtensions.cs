﻿using API.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Utility
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddMyAuth(this IServiceCollection services, Config.OAuth2Config oauthConfig)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => SetJwtOptions(options, oauthConfig));
            return services;
        }


        public static IApplicationBuilder UseMyAuth(this IApplicationBuilder app)
        {
            return app
                .UseAuthentication()
                .UseAuthorization();
        }


        private static void SetJwtOptions(JwtBearerOptions options, Config.OAuth2Config oauthConfig)
        {
            options.Authority = oauthConfig.Authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
            };
            options.RequireHttpsMetadata = false;
        }
    }
}