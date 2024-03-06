namespace Core.Domain.FileStorage.Ports;
public interface IImagesRepository
{
    Task<int> Insert(int originalFileId, int? largeFileId, int? mediumFileId, int thumbnailFileId);
}
