using Core.Common.Database;
using Core.Images.Storage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Core.Music.ListAlbumsQuery;

namespace Core.Music;
public class ListAlbumsQuery : IRequest<IEnumerable<ResponseItemDto>>
{
    public AlbumType? AlbumType { get; set; }

    public class ResponseItemDto
    {
        public AlbumType AlbumType { get; set; }
        public string Title { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public Uri AlbumArtUri { get; set; } = null!;
    }

    private class CommandHandler : IRequestHandler<ListAlbumsQuery, IEnumerable<ResponseItemDto>>
    {
        private readonly MyDbContext _dbContext;
        private readonly IImageLocator _imageLocator;

        public CommandHandler(MyDbContext dbContext, IImageLocator imageLocator)
        {
            _dbContext = dbContext;
            _imageLocator = imageLocator;
        }

        public async Task<IEnumerable<ResponseItemDto>> Handle(ListAlbumsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<AlbumEntity> query = _dbContext.Albums
                .Include(album => album.AlbumType)
                .Include(album => album.Image).ThenInclude(image => image.ThumbnailFile);

            if (request.AlbumType != null)
            {
                var albumType = await GetAlbumType(request.AlbumType.ToString()!);
                query = query.Where(album => album.AlbumType == albumType);
            }

            return (await query.ToListAsync()).Select(EntityToDto);
        }

        private ResponseItemDto EntityToDto(AlbumEntity entity)
        {
            return new ResponseItemDto
            {
                AlbumType = Enum.Parse<AlbumType>(entity.AlbumType.Name),
                Title = entity.Title,
                Artist = entity.Artist,
                ReleaseDate = entity.ReleaseDate,
                AlbumArtUri = _imageLocator.GetUri(entity.Image),
            };
        }

        private Task<AlbumTypeEntity> GetAlbumType(string name)
        {
            return _dbContext.AlbumTypes.SingleAsync(albumType => albumType.Name == name);
        }
    }
}
