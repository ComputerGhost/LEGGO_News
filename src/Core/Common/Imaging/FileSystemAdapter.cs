using Core.Startup;
using Microsoft.Extensions.Options;

namespace Core.Common.Imaging;
internal class FileSystemAdapter : IFileSystemPort
{
    private readonly string _storagePath = null!;

    public FileSystemAdapter(IOptions<CoreOptions> options)
    {
        _storagePath = options.Value.FileStoragePath;
    }

    public Task<Stream> LoadFile(string fileName)
    {
        var filePath = Path.Combine(_storagePath, fileName);
        return Task.FromResult<Stream>(File.OpenRead(filePath));
    }

    public Task SaveFile(string fileName, Stream inputStream)
    {
        var filePath = Path.Combine(_storagePath, fileName);
        var outputStream = File.Create(filePath);
        return inputStream.CopyToAsync(outputStream);
    }
}
