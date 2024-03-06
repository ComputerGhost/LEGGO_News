using Core.Startup;
using Downloads.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore(options =>
{
    builder.Configuration.GetSection("Core").Bind(options);
});

var app = builder.Build();

app.MapGet("/", DefaultHandler.Handle);
app.MapGet("{fileId:int}", FileByIdHandler.Handle);
app.MapGet("{filename:string}", FileByNameHandler.Handle);

app.Run();
