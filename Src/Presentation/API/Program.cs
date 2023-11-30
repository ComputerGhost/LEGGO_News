using API.Setup;
using API.Setup.Config;
using Core.Common.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration.Get<TopConfig>()!;

builder.Services.AddControllers();
builder.Services.AddMyCors();
builder.Services.AddMySwagger(config);
builder.Services.AddCore();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

await app.RunAsync();
