using API.Setup;
using API.Utility;
using Calendar.Setup;
using Database.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System;

namespace API
{
    public class Startup
    {
        readonly string AllowAllOriginsCors = "AllOrigins";

        public Config Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = (Config)configuration.Get(typeof(Config));
        }


        private void SetCorsOptions(CorsOptions options)
        {
            options.AddPolicy(AllowAllOriginsCors, builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
            });
        }

        private void SetMvcToUseJson(MvcOptions options)
        {
            options.Filters.Add(new ConsumesAttribute("application/json"));
            options.Filters.Add(new ProducesAttribute("application/json"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(new DatabaseConfiguration
            {
                ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
            });
            services.AddCalendar(Config.Calendar);

            services.AddControllers();
            services.AddCors(options => SetCorsOptions(options));
            services.AddMvc(options => SetMvcToUseJson(options));
            services.AddMySwagger(Config.OAuth2);
            services.AddMyAuth(Config.OAuth2);
            services.AddAutoMapper(typeof(MappingProfile));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseCors(AllowAllOriginsCors)
                .UseMyAuth()
                .UseEndpoints(conventions => conventions.MapControllers())
                .UseMySwagger();
        }
    }
}
