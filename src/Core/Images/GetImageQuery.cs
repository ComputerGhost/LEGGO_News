using Core.Common;
using Core.Common.Database;
using Core.ImageStorage;
using Core.Startup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Core.Images.GetImageQuery;

namespace Core.Images;
public class GetImageQuery : IRequest<ResponseDto>
{
    public GetImageQuery(int id, ImageWidth width)
    {
        Id = id;
        Width = width;
    }

    public int Id { get; set; }

    public ImageWidth Width { get; set; }

    public class ResponseDto
    {
        public string FileName { get; set; } = null!;
        public string MimeType { get; set; } = null!;
        public Stream Stream { get; set; } = null!;
    }

    private class CommandHandler : IRequestHandler<GetImageQuery, ResponseDto>
    {
        private readonly MyDbContext _dbContext;
        private readonly string _storagePath = null!;

        public CommandHandler(MyDbContext dbContext, IOptions<CoreOptions> options)
        {
            _dbContext = dbContext;
            _storagePath = options.Value.FileStoragePath;
        }

        public async Task<ResponseDto> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            var fileEntity = await GetFile(request.Id, request.Width);
            if (fileEntity == null)
            {
                throw new NotFoundException();
            }

            return new ResponseDto
            {
                FileName = fileEntity.PublicFileName,
                MimeType = GetMimeType(fileEntity.PublicFileName),
                Stream = LoadFile(fileEntity.PrivateFileName),
            };
        }

        private async Task<FileEntity?> GetFile(int imageId, ImageWidth width)
        {
            var fileIdColumn = width switch
            {
                ImageWidth.Thumbnail => "ThumbnailFileId",
                ImageWidth.Medium => "MediumFileId",
                ImageWidth.Large => "LargeFileId",
                ImageWidth.Original => "OriginalFileId",
                _ => throw new NotImplementedException()
            };

            var sql = $"SELECT Files.* FROM Images JOIN Files ON Images.{fileIdColumn} = Files.Id WHERE Images.Id = {{0}}";
            return await _dbContext.Files.FromSqlRaw(sql, imageId).SingleAsync();
        }

        private string GetMimeType(string publicFileName)
        {
            return Path.GetExtension(publicFileName) switch
            {
                ".jpeg" => "image/jpeg",
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                _ => throw new NotImplementedException("Cannot deduce mime type of hosted file."),
            };
        }

        private Stream LoadFile(string serverFileName)
        {
            return File.OpenRead(Path.Combine(_storagePath, serverFileName));
        }
    }
}
