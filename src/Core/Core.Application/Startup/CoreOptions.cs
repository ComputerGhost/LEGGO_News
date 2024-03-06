namespace Core.Application.Startup;
public class CoreOptions
{
    public string DatabaseConnectionString { get; set; } = null!;

    public int FileStoragePartitionSize { get; set; }

    public string FileStoragePath { get; set; } = null!;

    public string FileStorageUrl { get; set; } = null!;
}
