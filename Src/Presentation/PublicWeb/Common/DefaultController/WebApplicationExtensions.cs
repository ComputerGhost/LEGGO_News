namespace DefaultController.Common.DefaultController;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Set up the routes for the default controller.
    /// </summary>
    /// <remarks>
    /// This should be placed after other routing rules.
    /// </remarks>
    public static WebApplication UseDefaultController(this WebApplication app)
    {
        app.MapControllerRoute(
            name: "DefaultController",
            pattern: "{**path}",
            defaults: new { controller = "Default", action = "Default" });

        return app;
    }
}
