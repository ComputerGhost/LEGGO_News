using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Media
{
    public class ImageOperations
    {
        public static async Task<ImageData> Load(Stream stream)
        {
            (Image image, IImageFormat format) = await Image.LoadWithFormatAsync(stream);

            image.Metadata.ExifProfile = null;

            return new ImageData {
                _InternalImage = image,
                Size = new Size(image.Width, image.Height),
                MimeType = format.DefaultMimeType,
                Extension = format.FileExtensions.FirstOrDefault(),
            };
        }

        public static async Task Save(ImageData imageData, string path)
        {
            await imageData._InternalImage.SaveAsync(path);
        }

        /// <summary>
        /// Saves a file multiple times, once for each size.
        /// </summary>
        /// <returns>The largest size saved</returns>
        public static async Task<string> SaveVariants(ImageData imageData, string basePath)
        {
            var biggest = BiggestVariant(imageData);

            await Save(imageData, Path.Combine(basePath, imageData.FileName));
            if (biggest == MediaSize.Original)
                return biggest;

            using (var resized = Resize(imageData, MediaSize.Thumbnail))
                await Save(resized, Path.Combine(basePath, "thumbnail", imageData.FileName));
            if (biggest == MediaSize.Thumbnail)
                return biggest;

            using (var resized = Resize(imageData, MediaSize.Small))
                await Save(resized, Path.Combine(basePath, "small", imageData.FileName));
            if (biggest == MediaSize.Small)
                return biggest;

            using (var resized = Resize(imageData, MediaSize.Medium))
                await Save(resized, Path.Combine(basePath, "medium", imageData.FileName));
            if (biggest == MediaSize.Medium)
                return biggest;

            using (var resized = Resize(imageData, MediaSize.Large))
                await Save(resized, Path.Combine(basePath, "large", imageData.FileName));
            if (biggest == MediaSize.Large)
                return biggest;

            // This line isn't needed logically, but the compiler warns without it.
            return biggest;
        }

        public static ImageData Resize(ImageData imageData, string sizeName)
        {
            var oldSize = imageData.Size;

            Rectangle crop = new Rectangle(0, 0, oldSize.Width, oldSize.Height);
            Size newSize;
            switch (sizeName) {
                case MediaSize.Large:
                    newSize = new Size(1080, 0);
                    break;
                case MediaSize.Medium:
                    newSize = new Size(720, 0);
                    break;
                case MediaSize.Small:
                    newSize = new Size(540, 0);
                    break;
                case MediaSize.Thumbnail:
                    if (oldSize.Width > oldSize.Height)
                        crop = new Rectangle((oldSize.Width - oldSize.Height) / 2, 0, oldSize.Height, oldSize.Height);
                    if (oldSize.Height > oldSize.Width)
                        crop = new Rectangle(0, (oldSize.Height - oldSize.Width) / 2, oldSize.Width, oldSize.Width);
                    newSize = new Size(152, 152);
                    break;
                default:
                    throw new InvalidOperationException("New size for image resize is invalid.");
            }
            if (newSize.Height == 0)
                newSize.Height = oldSize.Height * newSize.Width / oldSize.Width;

            var newImage = imageData._InternalImage.Clone(operation => operation
                .Crop(crop)
                .Resize(newSize.Width, newSize.Height)
            );

            return new ImageData {
                FileName = imageData.FileName,
                Size = imageData.Size,
                MimeType = imageData.MimeType,
                _InternalImage = newImage
            };
        }

        public static string BiggestVariant(ImageData imageData)
        {
            // Minimum height is 152
            if (imageData.Size.Height < 152)
                return MediaSize.Original;

            // Otherwise, size is based on width
            if (imageData.Size.Width > 1080)
                return MediaSize.Large;
            if (imageData.Size.Width > 720)
                return MediaSize.Medium;
            if (imageData.Size.Width > 540)
                return MediaSize.Small;
            if (imageData.Size.Width > 152)
                return MediaSize.Thumbnail;

            return MediaSize.Original;
        }
    }
}
