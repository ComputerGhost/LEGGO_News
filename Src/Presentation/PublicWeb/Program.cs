using Core.Common.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddMvc();
services.AddCore();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

await app.RunAsync();
