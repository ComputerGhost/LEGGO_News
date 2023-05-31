using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Net;

namespace CMS.Setup
{
    public static class Authentication
    {
        public static void AddMyAuthentication(this IServiceCollection services, Config config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddOpenIdConnect(options => ConfigureOpenIdConnect(options, config));
        }

        private static void ConfigureOpenIdConnect(OpenIdConnectOptions options, Config config)
        {
            options.Authority = config.OAuth.Authority;
            options.ClientId = config.OAuth.ClientId;
            options.ClientSecret = config.OAuth.ClientSecret;
            options.RequireHttpsMetadata = config.OAuth.RequireHttpsMetadata ?? true;
            options.ResponseType = "code";
            options.SaveTokens = true;

            options.Events.OnRedirectToIdentityProvider = context =>
            {
                // Ajax should get a 401, not a redirect to login page.
                if (context.Request.Headers.XRequestedWith == "XMLHttpRequest")
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.HandleResponse();
                }
                return Task.CompletedTask;
            };
        }
    }
}
