using Calendar.Setup;
using Public.Setup;
using Public.Utility;

var builder = WebApplication.CreateBuilder(args);
var config = (Config)builder.Configuration.Get(typeof(Config));

builder.Services.AddCalendar(config.Calendar);
builder.Services.AddRouting(options =>
{
    options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
});
builder.Services.AddRazorPages();
builder.Services.AddDependencyInjection(config);
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller:slugify}/{action:slugify}/{id?}",
    defaults: new { controller = "Home", action = "Index" });


await app.RunAsync();
