using Core.Infrastructure.Startup;

namespace Core.Application.Startup;

// This is a proxy object for InfrastructureOptions.
public class CoreOptions
{
    internal readonly InfrastructureOptions InfrastructureOptions;

    public CoreOptions(InfrastructureOptions infrastructureOptions)
    {
        InfrastructureOptions = infrastructureOptions;
    }

    public string DatabaseConnectionString
    {
        get => InfrastructureOptions.DatabaseConnectionString;
        set => InfrastructureOptions.DatabaseConnectionString = value;
    }

    public int FileStoragePartitionSize
    {
        get => InfrastructureOptions.FileStoragePartitionSize;
        set => InfrastructureOptions.FileStoragePartitionSize = value;
    }

    public string FileStoragePath
    {
        get => InfrastructureOptions.FileStoragePath; 
        set => InfrastructureOptions.FileStoragePath = value;
    }

    public string FileStorageUrl
    { 
        get => InfrastructureOptions.FileStorageUrl; 
        set => InfrastructureOptions.FileStorageUrl = value;
    }
}
