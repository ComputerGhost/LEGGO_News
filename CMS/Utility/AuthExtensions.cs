using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.Utility
{
    public static class AuthExtensions
    {

        private static Task InterceptProxyRedirects(RedirectContext context)
        {
            // See <https://stackoverflow.com/questions/36795259/>.
            var isAjaxRequest = context.Request.Headers != null && context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjaxRequest)
            {
                context.Response.Headers.Remove("Set-Cookie");
                // Intentionally breaking the standard and not sending a WWW-Authenticate header with the 401.
                context.Response.StatusCode = 401;
                context.HandleResponse();
            }
            return Task.CompletedTask;
        }

        private static Task SaveJwtToCookie(string jwtCookieName, UserInformationReceivedContext context)
        {
            var jwt = JsonSerializer.Serialize(context.User.RootElement);
            context.HttpContext.Response.Cookies.Append(jwtCookieName, jwt);
            return Task.CompletedTask;
        }

        private static void SetOidcOptions(OpenIdConnectOptions options, Config.OIDCConfig oidcConfig)
        {
            options.Authority = oidcConfig.Authority;
            options.ClientId = oidcConfig.ClientId;
            options.ClientSecret = oidcConfig.ClientSecret;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.SaveTokens = true;
            options.UsePkce = false;
            options.UseTokenLifetime = true;
            options.Events = new OpenIdConnectEvents
            {
                OnUserInformationReceived = (context) => SaveJwtToCookie(oidcConfig.JwtCookieName, context),
                OnRedirectToIdentityProvider = (context) => InterceptProxyRedirects(context)
            };
            RemoveHttpsRequirement(options);

            [Conditional("DEBUG")]
            static void RemoveHttpsRequirement(OpenIdConnectOptions oidcOptions)
            {
                oidcOptions.RequireHttpsMetadata = false;
            }
        }

        public static void AddMyAuth(this IServiceCollection services, Config.OIDCConfig oidcConfig)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => SetOidcOptions(options, oidcConfig));
        }


        private static async Task ChallengeIfNotAuthenticated(HttpContext context, Func<Task> next)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                await context.ChallengeAsync();
                return;
            }
            await next();
        }

        public static IApplicationBuilder UseMyAuth(this IApplicationBuilder app)
        {
            return app
                .UseAuthentication()
                .Use(ChallengeIfNotAuthenticated);
        }

    }
}
