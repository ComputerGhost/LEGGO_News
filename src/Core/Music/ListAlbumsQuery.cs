using Core.Common.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Core.Music.ListAlbumsQuery;

namespace Core.Music;
public class ListAlbumsQuery : IRequest<IEnumerable<ResponseItemDto>>
{
    public AlbumType? AlbumType { get; set; }

    public class ResponseItemDto
    {
        public int Id { get; set; }
        public AlbumType AlbumType { get; set; }
        public string Title { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public int AlbumArtImageId { get; set; }
    }

    private class CommandHandler : IRequestHandler<ListAlbumsQuery, IEnumerable<ResponseItemDto>>
    {
        private readonly MyDbContext _dbContext;

        public CommandHandler(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ResponseItemDto>> Handle(ListAlbumsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<AlbumEntity> query = _dbContext.Albums
                .Include(album => album.AlbumType);

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
                Id = entity.Id,
                AlbumType = Enum.Parse<AlbumType>(entity.AlbumType.Name),
                Title = entity.Title,
                Artist = entity.Artist,
                ReleaseDate = entity.ReleaseDate,
                AlbumArtImageId = entity.ImageId,
            };
        }

        private Task<AlbumTypeEntity> GetAlbumType(string name)
        {
            return _dbContext.AlbumTypes.SingleAsync(albumType => albumType.Name == name);
        }
    }
}
