using Core.Startup;
using Microsoft.Extensions.Options;

namespace Core.ImageStorage.Repositories;

[ServiceImplementation]
internal class FileSystem : IFileSystem
{
    private readonly string _storagePath = null!;

    public FileSystem(IOptions<CoreOptions> options)
    {
        _storagePath = options.Value.FileStoragePath;
    }

    public async Task Create(string partition, string fileName, Stream inputStream)
    {
        var filePath = Path.Combine(_storagePath, partition, fileName);
        using var fileStream = File.Create(filePath);
        await inputStream.CopyToAsync(fileStream);
    }
}
