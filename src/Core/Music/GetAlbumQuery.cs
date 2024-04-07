using Core.Common.Database;
using Core.Images.Storage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Core.Music.GetAlbumQuery;

namespace Core.Music;
public class GetAlbumQuery : IRequest<ResponseDto>
{
    public GetAlbumQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public class ResponseDto
    {
        public AlbumType AlbumType { get; set; }

        public string Title { get; set; } = null!;

        public string Artist { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        public Uri AlbumArtUri { get; set; } = null!;
    }

    private class CommandHandler : IRequestHandler<GetAlbumQuery, ResponseDto>
    {
        private readonly MyDbContext _dbContext;
        private readonly IImageLocator _imageLocator;

        public CommandHandler(MyDbContext dbContext, IImageLocator imageLocator)
        {
            _dbContext = dbContext;
            _imageLocator = imageLocator;
        }

        public async Task<ResponseDto> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
        {
            var albumEntity = await _dbContext.Albums
                .Include(album => album.AlbumType)
                .Include(album => album.Image).ThenInclude(image => image.ThumbnailFile)
                .SingleAsync(album => album.Id == request.Id);

            return new ResponseDto
            {
                AlbumType = Enum.Parse<AlbumType>(albumEntity.AlbumType.Name),
                Title = albumEntity.Title,
                Artist = albumEntity.Artist,
                ReleaseDate = albumEntity.ReleaseDate,
                AlbumArtUri = _imageLocator.GetUri(albumEntity.Image),
            };
        }
    }
}
