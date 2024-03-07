namespace Core.Infrastructure.Startup;
public class InfrastructureOptions
{
    public string DatabaseConnectionString { get; set; } = null!;

    public string FileStoragePath { get; set; } = null!;

    public string FileStorageUrl { get; set; } = null!;
}
