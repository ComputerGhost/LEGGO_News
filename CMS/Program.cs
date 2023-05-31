using CMS.Setup;
using DataAccess;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);
var config = (Config)builder.Configuration.Get(typeof(Config))!;

IdentityModelEventSource.ShowPII = true;

builder.Services.AddRouting();
builder.Services.AddRazorPages();
builder.Services.AddDataAccess();
builder.Services.AddDependencyInjection(config);
builder.Services.AddMyAuthentication(config);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();


await app.RunAsync();
