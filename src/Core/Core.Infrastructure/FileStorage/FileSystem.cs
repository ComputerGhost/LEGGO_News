using Core.Domain.FileStorage.Ports;
using Core.Infrastructure.Startup;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.FileStorage;

[ServiceImplementation]
internal class FileSystem : IFileSystem
{
    private readonly int _partitionSize;
    private readonly string _storagePath = null!;

    public FileSystem(IOptions<InfrastructureOptions> options)
    {
        _partitionSize = options.Value.FileStoragePartitionSize;
        _storagePath = options.Value.FileStoragePath;
    }

    public async Task Create(Stream inputStream, int fileId)
    {
        var filePath = GetFilePath(fileId);
        using var fileStream = File.Create(filePath);
        await inputStream.CopyToAsync(fileStream);
    }

    private string GetFilePath(int fileId)
    {
        var partitionNumber = fileId / _partitionSize;
        return $"{_storagePath}/p-{partitionNumber}/f-{fileId}.dat";
    }
}
