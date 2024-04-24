using Core.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddCore(options =>
{
    builder.Configuration.GetSection("Core").Bind(options);
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

await app.RunAsync();
