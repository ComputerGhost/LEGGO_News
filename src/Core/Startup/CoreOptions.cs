namespace Core.Startup;
public class CoreOptions
{
    public string DatabaseConnectionString { get; set; } = null!;

    public string FileStoragePath { get; set; } = null!;

    public Uri FileStorageUri { get; set; } = null!;
}
