using FluentValidation;
using System.Text.RegularExpressions;

namespace Core.Images;
public class ImageUpload
{
    public string FileName { get; set; } = null!;
    public Stream Stream { get; set; } = null!;

    internal class Validator : AbstractValidator<ImageUpload>
    {
        public Validator()
        {
            RuleFor(upload => upload.FileName)
                .NotEmpty()
                .MaximumLength(255)
                .Must(IsValidFileName).WithMessage("The filename is not valid.");

            RuleFor(upload => upload.Stream)
                .Must((imageUpload, stream, context) => CanLoadImage(context, imageUpload))
                .WithMessage("Unable to load image. {Exceptionmessage}");
        }

        private bool CanLoadImage(ValidationContext<ImageUpload> context, ImageUpload imageUpload)
        {
            try
            {
                var _ = new Image(imageUpload.Stream, imageUpload.FileName);

                // Reset stream because we're throwing away the Image we just created.
                imageUpload.Stream.Seek(0, SeekOrigin.Begin);

                return true;
            }
            catch (Exception ex)
            {
                context.MessageFormatter.AppendArgument("ExceptionMessage", ex.Message);
                return false;
            }
        }

        private bool IsValidFileName(string fileName)
        {
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            var pattern = "[" + Regex.Escape(invalidChars) + "]";
            return !Regex.IsMatch(fileName, pattern);
        }
    }
}
