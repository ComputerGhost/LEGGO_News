using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Web.Utility;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        private void AddSlugifyConstraint(RouteOptions options)
        {
            options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(option =>
            {
                option.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            });

            services.AddAutoMapper(typeof(Business.Setup.MappingProfile));
            services.AddRouting(options => AddSlugifyConstraint(options));
            services.AddControllersWithViews();

            Business.Setup.DependencyInjection.Configure(services);
        }


        private void BuildEndPoints(IEndpointRouteBuilder routes)
        {
            routes.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify}/{action:slugify}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
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
    }
}
