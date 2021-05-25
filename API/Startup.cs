using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        readonly string AllowAllOriginsCors = "AllOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(option =>
            {
                option.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            });

            services.AddControllers();

            services.AddCors(cfg =>
            {
                cfg.AddPolicy(AllowAllOriginsCors, builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                });
            });

            services.AddMvc(cfg =>
            {
                cfg.Filters.Add(new ConsumesAttribute("application/json"));
                cfg.Filters.Add(new ProducesAttribute("application/json"));
            });

            services.AddSwaggerGen(cfg =>
            {
                cfg.DocumentFilter<JsonPatchDocumentFilter>();
                cfg.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowAllOriginsCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
