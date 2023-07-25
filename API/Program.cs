using API.Setup;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var config = (Config)builder.Configuration.Get(typeof(Config))!;

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cors =>
    {
        cors.AllowAnyOrigin();
        cors.AllowAnyMethod();
        cors.AllowAnyHeader();
    });
});
builder.Services.AddDataAccess();
builder.Services.AddDependencyInjection(config);
builder.Services.AddMySwagger(config);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => builder.Configuration.Bind("JwtSettings", options));


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
