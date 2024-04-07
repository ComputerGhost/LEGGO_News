using Core.Common.Database;

namespace Core.Images.Storage;
internal interface IImageSaver
{
    Task<ImageEntity> Create(ImageUpload imageUpload);
}
