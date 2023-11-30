namespace API.Setup;

internal static class CorsSetup
{
    public static void AddMyCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(cors =>
            {
                cors.AllowAnyHeader();
                cors.AllowAnyMethod();
                cors.AllowAnyOrigin();
            });
        });
    }
}
