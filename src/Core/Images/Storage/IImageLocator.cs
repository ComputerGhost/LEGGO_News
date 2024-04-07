using Core.Common.Database;

namespace Core.Images.Storage;
internal interface IImageLocator
{
    public Uri GetUri(ImageEntity imageEntity);
}
