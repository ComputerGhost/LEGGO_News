using Core.Domain.Common.Entities;
using Core.Domain.Imaging.Ports;
using Core.Domain.Imaging.Subsystems;
using Core.Domain.Startup;
using SkiaSharp;

namespace Core.Domain.Imaging;

[ServiceImplementation]
internal class ImagingFacade : IImagingFacade
{
    private readonly IFileSystemPort _fileSystemAdapter;

    public ImagingFacade(IFileSystemPort fileSystemAdapter)
    {
        _fileSystemAdapter = fileSystemAdapter;
    }

    public bool CanLoadImage(Stream stream)
    {
        return new ValidationSubsystem().CanLoadImage(stream);
    }

    public SKEncodedImageFormat DeduceFormat(string fileName)
    {
        return new FormattingSubsystem(fileName).Execute();
    }

    public bool IsSupportedFileExtension(string extension)
    {
        return new ValidationSubsystem().IsSupportedFileExtension(extension);
    }

    public async Task<ImageEntity> SaveToFileSystem(string fileName, Stream stream)
    {
        var format = new FormattingSubsystem(fileName).Execute();
        var savingJob = new SavingSubsystem(_fileSystemAdapter, fileName, stream, format);
        return await savingJob.Execute();
    }
}
