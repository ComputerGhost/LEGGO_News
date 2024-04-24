using Core.Domain.Common.Entities;

namespace Core.Domain.Imaging.Ports;
public interface IImagesDatabasePort
{
    Task<FileEntity?> FetchFile(int fileId);
    Task<ImageEntity?> FetchImage(int imageId);
}
