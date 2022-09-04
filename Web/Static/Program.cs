using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapGet("/", () => "This is the LEGGO News file store.");

await app.RunAsync();
