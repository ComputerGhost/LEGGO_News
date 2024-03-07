using Core.Domain.FileStorage.Ports;
using Core.Infrastructure.Startup;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.FileStorage;

[ServiceImplementation]
internal class FileSystem : IFileSystem
{
    private readonly string _storagePath = null!;

    public FileSystem(IOptions<InfrastructureOptions> options)
    {
        _storagePath = options.Value.FileStoragePath;
    }

    public async Task Create(Stream inputStream, int fileId)
    {
        var filePath = $"{_storagePath}/f-{fileId}.dat";
        using var fileStream = File.Create(filePath);
        await inputStream.CopyToAsync(fileStream);
    }
}
