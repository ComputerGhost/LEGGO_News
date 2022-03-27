using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ProxyKit;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProxy();

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = Configuration["OIDC:Authority"];
                    options.ClientId = Configuration["OIDC:ClientId"];
                    options.ClientSecret = Configuration["OIDC:ClientSecret"];
                    options.GetClaimsFromUserInfoEndpoint = true;
#if DEBUG
                    options.RequireHttpsMetadata = false;
#endif
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SaveTokens = true;
                    options.UsePkce = false;
                    options.UseTokenLifetime = true;

                    options.Events = new OpenIdConnectEvents
                    {
                        OnUserInformationReceived = (UserInformationReceivedContext context) =>
                        {
                            var jwt = JsonSerializer.Serialize(context.User.RootElement);
                            context.HttpContext.Response.Cookies.Append(Configuration["OIDC:JwtCookieName"], jwt);
                            return Task.CompletedTask;
                        },
                        // Can I handle this in React? 🤔
                        OnRedirectToIdentityProvider = (RedirectContext context) =>
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
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    await context.ChallengeAsync();
                    return;
                }
                await next();
            });

            app.Map("/api", api =>
            {
                api.RunProxy(async context =>
                {
                    var forwardContext = context.ForwardTo(Configuration["APIBaseUri"]);
                    var token = await context.GetTokenAsync("access_token");
                    forwardContext.UpstreamRequest.SetBearerToken(token);
                    return await forwardContext.Send();
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
