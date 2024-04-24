using Core.Domain.Imaging;
using Core.Domain.Imaging.Ports;
using FluentValidation;

namespace Core.Application.Common.Models;
public class ImageUpload
{
    public string FileName { get; set; } = null!;
    public Stream Stream { get; set; } = null!;

    internal class Validator : AbstractValidator<ImageUpload>
    {
        private readonly IFileSystemPort _fileSystemAdapter;

        public Validator(IFileSystemPort fileSystemAdapter)
        {
            _fileSystemAdapter = fileSystemAdapter;

            RuleFor(upload => upload.FileName)
                .Cascade(CascadeMode.Stop)
                .Length(1, 255)
                .Must(IsValidFileName).WithMessage("The file name is invalid.")
                .Must(HasValidFileExtension).WithMessage("The file extension is not supported.");

            RuleFor(upload => upload.Stream)
                .Must(ImageValidation.CanLoadImage).WithMessage("The image cannot be loaded.");
        }

        private bool IsValidFileName(string fileName)
        {
            return _fileSystemAdapter.IsValidFileName(fileName);
        }

        private bool HasValidFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return ImageValidation.IsSupportedFileExtension(extension);
        }
    }
}
