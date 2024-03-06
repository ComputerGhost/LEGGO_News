namespace Core.Domain.FileStorage.Ports;
public interface IFilesRepository
{
    Task<int> Insert(string fileName);
}
