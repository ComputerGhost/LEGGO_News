namespace Core.ImageStorage;
public enum ImageWidth
{
    Original,
    Large,
    Medium,
    Thumbnail,
}

internal static class ImageWidthExtensions
{
    public static int ToInt(this ImageWidth imageWidth)
    {
        return imageWidth switch
        {
            ImageWidth.Original => -1,
            ImageWidth.Large => 1024,
            ImageWidth.Medium => 300,
            ImageWidth.Thumbnail => 150,

            // This should never happen, but it's needed to resolve the CS8524 warning.
            _ => throw new Exception("Unexpected image width.")
        }; ;
    }
}
