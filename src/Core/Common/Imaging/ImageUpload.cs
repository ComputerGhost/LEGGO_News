using FluentValidation;
using SkiaSharp;
using System.Text.RegularExpressions;

namespace Core.Common.Imaging;
public class ImageUpload
{
    public string FileName { get; set; } = null!;
    public Stream Stream { get; set; } = null!;

    internal class Validator : AbstractValidator<ImageUpload>
    {
        private static string[] ReservedFileNames = [
            "AUX", "CON", "NUL", "PRN",
            "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM¹", "COM²", "COM³",
            "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "LPT¹", "LPT²", "LPT³"
        ];

        public Validator()
        {
            RuleFor(upload => upload.FileName)
                .Cascade(CascadeMode.Stop)
                .Length(0, 255)
                .Must(IsNotReservedFileName).WithMessage("The file name cannot be a reserved a file name.")
                .Must(IsValidFileName).WithMessage("The file name is not valid.")
                .Must(IsValidFileExtension).WithMessage("The file extension is not supported.");

            RuleFor(upload => upload.Stream)
                .Must(CanLoadImage).WithMessage("Unable to load image.");
        }

        private bool CanLoadImage(Stream stream)
        {
            try
            {
                using var image = SKImage.FromEncodedData(stream);
                if (image == null)
                {
                    throw new Exception();
                }
;
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }

        private bool IsValidFileExtension(string fileName)
        {
            try
            {
                var extension = Path.GetExtension(fileName);
                if (!FormattingSubsystem.SupportedExtensions.Contains(extension))
                {
                    throw new Exception();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidFileName(string fileName)
        {
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            var pattern = "[" + Regex.Escape(invalidChars) + "]";
            return !Regex.IsMatch(fileName, pattern);
        }

        private bool IsNotReservedFileName(string fileName)
        {
            return !ReservedFileNames.Contains(fileName.ToUpper());
        }
    }
}
