using Core.Application.Common.Exceptions;
using Core.Domain.Common.Entities;
using Core.Domain.Imaging.Enums;
using Core.Domain.Imaging.Ports;
using MediatR;
using MediatR.Behaviors.Authorization;
using static Core.Application.Images.GetImageQuery;

namespace Core.Application.Images;
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

    internal class Authorizer : AbstractRequestAuthorizer<GetImageQuery>
    {
        public override void BuildPolicy(GetImageQuery request)
        {
            // Securing images will be too complex with the current design.
            // We will allow this to be hacked so images from unpublished content may be discovered.
            // The cost of the hack is less than the cost of fixing it.
        }
    }

    internal class Handler : IRequestHandler<GetImageQuery, ResponseDto>
    {
        private readonly IImagesDatabasePort _databaseAdapter;
        private readonly IFileSystemPort _fileSystemPort;

        public Handler(IImagesDatabasePort databasePort, IFileSystemPort fileSystemPort)
        {
            _databaseAdapter = databasePort;
            _fileSystemPort = fileSystemPort;
        }

        public async Task<ResponseDto> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            var imageEntity = await _databaseAdapter.FetchImage(request.Id);
            if (imageEntity == null)
            {
                throw new NotFoundException();
            }

            var fileEntry = await GetFile(imageEntity, request.Width);
            if (fileEntry == null)
            {
                throw new NotFoundException();
            }

            return new ResponseDto
            {
                FileName = fileEntry.PublicFileName,
                MimeType = GetMimeType(fileEntry.PublicFileName),
                Stream = await _fileSystemPort.LoadFile(fileEntry.PrivateFileName),
            };
        }

        private async Task<FileEntity?> GetFile(ImageEntity imageEntity, ImageWidth width)
        {
            var fileId = width switch
            {
                ImageWidth.Thumbnail => imageEntity.ThumbnailFileId,
                ImageWidth.Medium => imageEntity.MediumFileId,
                ImageWidth.Large => imageEntity.LargeFileId,
                ImageWidth.Original => imageEntity.OriginalFileId,
                _ => throw new NotImplementedException()
            };

            if (fileId == null)
            {
                return null;
            }

            return await _databaseAdapter.FetchFile(fileId.Value);
        }

        private string GetMimeType(string publicFileName)
        {
            return Path.GetExtension(publicFileName) switch
            {
                ".jpeg" => "image/jpeg",
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                _ => throw new NotImplementedException("Cannot deduce mime type of hosted file."),
            };
        }
    }
}
