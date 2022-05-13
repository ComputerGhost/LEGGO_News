using Database.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Web.Utility;

namespace Web
{
    public class Startup
    {
        public Config Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = (Config)configuration.Get(typeof(Config));
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(new DatabaseConfiguration
            {
                ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
            });
            services.AddCalendar(Config.Calendar);

            services.AddRouting(options => AddSlugifyConstraint(options));
            services.AddControllersWithViews();
        }

        private void AddSlugifyConstraint(RouteOptions options)
        {
            options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(routes => BuildEndPoints(routes));
        }

        private void BuildEndPoints(IEndpointRouteBuilder routes)
        {
            routes.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify}/{action:slugify}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
        }
    }
}
