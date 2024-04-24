namespace Core.Domain.Imaging.Ports;
public interface IFileSystemPort
{
    public bool IsValidFileName(string fileName);

    public Task<Stream> LoadFile(string fileName);

    public Task SaveFile(string fileName, Stream stream);
}
