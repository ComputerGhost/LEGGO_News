using Core.Domain.FileStorage.Ports;

namespace Core.Domain.FileStorage;
public class FileWriter
{
    private readonly IFilesRepository _filesRepository;
    private readonly IFileSystem _fileSystem;

    public FileWriter(IFilesRepository filesRepository, IFileSystem fileSystem)
    {
        _filesRepository = filesRepository;
        _fileSystem = fileSystem;
    }

    public async Task<int> CreateFile(Stream stream, string fileName)
    {
        var fileId = await CreateFileRecord(fileName);
        await SaveFileData(stream, fileId);
        return fileId;
    }

    private Task<int> CreateFileRecord(string fileName)
    {
        return _filesRepository.Insert(fileName);
    }

    private async Task SaveFileData(Stream stream, int fileId)
    {
        await _fileSystem.Create(stream, fileId);
    }
}
