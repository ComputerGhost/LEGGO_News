namespace Core.ImageStorage.Repositories;
internal interface IImagesRepository
{
    public Task<int> Insert(int originalFileId, int? largeFileId, int? mediumFileId, int thumbnailFileId);
}
