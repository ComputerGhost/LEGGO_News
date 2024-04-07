using Core.Common.Database;
using Core.Startup;

namespace Core.Images.Storage;

[ServiceImplementation]
internal class ImageLocator : IImageLocator
{
    public Uri GetUri(ImageEntity imageEntity)
    {
        throw new NotImplementedException();
    }
}
