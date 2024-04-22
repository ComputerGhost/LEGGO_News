using Core.Common.Database;
using Core.Startup;
using SkiaSharp;

namespace Core.Common.Imaging;

[ServiceImplementation]
internal class ImagingFacade : IImagingFacade
{
    private readonly IFileSystemPort _fileSystemPort;

    public ImagingFacade(IFileSystemPort fileSystemPort)
    {
        _fileSystemPort = fileSystemPort;
    }

    public SKEncodedImageFormat DeduceFormat(string fileName)
    {
        return new FormattingSubsystem(fileName).Execute();
    }

    public async Task<ImageEntity> SaveToFileSystem(ImageUpload imageUpload)
    {
        var format = new FormattingSubsystem(imageUpload.FileName).Execute();
        var savingJob = new SavingSubsystem(_fileSystemPort, imageUpload.FileName, imageUpload.Stream, format);
        return await savingJob.Execute();
    }
}
