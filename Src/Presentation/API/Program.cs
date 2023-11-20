using Core.Common.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddSwaggerGen();
services.AddCore();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(config => config
    .AllowAnyOrigin()
    .AllowAnyMethod());
app.MapControllers();

await app.RunAsync();
