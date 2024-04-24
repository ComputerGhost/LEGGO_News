using Core.Domain.Imaging.Ports;
using Core.Domain.Startup;
using Core.Infrastructure.Startup;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.Adapters;

[ServiceImplementation]
internal class FileSystemAdapter : IFileSystemPort
{
    private static string[] _reservedFileNames = [
        "AUX", "CON", "NUL", "PRN",
            "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM¹", "COM²", "COM³",
            "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "LPT¹", "LPT²", "LPT³"
    ];

    private static char[] _invalidFileNameChars = Path.GetInvalidFileNameChars();

    private readonly string _storagePath = null!;

    public FileSystemAdapter(IOptions<InfrastructureOptions> options)
    {
        _storagePath = options.Value.FileStoragePath;
    }

    public bool IsValidFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return false;
        }

        if (fileName.Length > 255)
        {
            return false;
        }

        if (_invalidFileNameChars.Any(fileName.Contains))
        {
            return false;
        }

        if (_reservedFileNames.Contains(fileName.ToUpper()))
        {
            return false;
        }

        return true;
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
