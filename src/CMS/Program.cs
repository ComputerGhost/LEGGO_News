var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddMvc();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

await app.RunAsync();
