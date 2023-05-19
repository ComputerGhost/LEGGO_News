using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

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
        }
    }
}
