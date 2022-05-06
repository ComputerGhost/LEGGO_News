using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace API.Utility
{
    public static class SwaggerExtensions
    {

        private static void SetSwaggerOptions(SwaggerGenOptions options)
        {
            options.DocumentFilter<JsonPatchDocumentFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml"));
        }

        public static void AddMySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => SetSwaggerOptions(options));
        }


        public static void UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

    }
}
