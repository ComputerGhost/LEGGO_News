using API.Setup;
using API.Utility;
using Calendar.Setup;
using Database.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var config = (Config)builder.Configuration.Get(typeof(Config));

builder.Services.AddDatabase(new DatabaseConfiguration
{
    ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
});
builder.Services.AddCalendar(config.Calendar);
builder.Services.AddControllers();
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(cors =>
    {
        cors.AllowAnyOrigin();
        cors.AllowAnyMethod();
        cors.AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<JsonPatchDocumentFilter>();
    options.AddMySecurityDefinition(new Uri(config.JwtSettings.Authority));
    options.AddMySecurityRequirement();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => builder.Configuration.Bind("JwtSettings", options));
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();


await app.RunAsync();
