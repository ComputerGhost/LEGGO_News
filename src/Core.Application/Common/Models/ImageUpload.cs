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
        private readonly IImagingFacade _imagingFacade;

        public Validator(IFileSystemPort fileSystemAdapter, IImagingFacade imagingFacade)
        {
            _fileSystemAdapter = fileSystemAdapter;
            _imagingFacade = imagingFacade;

            RuleFor(upload => upload.FileName)
                .Cascade(CascadeMode.Stop)
                .Length(1, 255)
                .Must(IsValidFileName).WithMessage("The file name is invalid.")
                .Must(HasValidFileExtension).WithMessage("The file extension is not supported.");

            RuleFor(upload => upload.Stream)
                .Must(CanLoadImage).WithMessage("The image cannot be loaded.");
        }

        private bool IsValidFileName(string fileName)
        {
            return _fileSystemAdapter.IsValidFileName(fileName);
        }

        private bool HasValidFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return _imagingFacade.IsSupportedFileExtension(extension);
        }

        public bool CanLoadImage(Stream stream)
        {
            return _imagingFacade.CanLoadImage(stream);
        }
    }
}
