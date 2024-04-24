using Core.Domain.Common.Entities;
using Core.Domain.Imaging.Ports;
using Core.Domain.Startup;
using Core.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Adapters;

[ServiceImplementation]
internal class ImagesDatabaseAdapter : IImagesDatabasePort
{
    private readonly MyDbContext _dbContext;

    public ImagesDatabaseAdapter(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<FileEntity?> FetchFile(int fileId)
    {
        return _dbContext.Files.SingleOrDefaultAsync(x => x.Id == fileId);
    }

    public Task<ImageEntity?> FetchImage(int imageId)
    {
        return _dbContext.Images.SingleOrDefaultAsync(x => x.Id == imageId);
    }
}
