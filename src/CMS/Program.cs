using Core.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
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
