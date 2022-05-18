using CMS;
using CMS.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var config = (Config)builder.Configuration.Get(typeof(Config));

builder.Services.AddMyProxy();
builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = "ClientApp/build";
});
builder.Services.AddMyAuth(config.OIDC);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMyAuth();
app.Map("/api", api => api.UseMyProxy(config.APIBaseUri))
    .UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";
        if (builder.Environment.IsDevelopment())
        {
            spa.UseReactDevelopmentServer(npmScript: "start");
        }
    });


await app.RunAsync();
