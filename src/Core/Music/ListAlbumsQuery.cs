using Core.Common.Database;
using Core.Startup;
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

    internal interface IDatabasePort
    {
        Task<AlbumTypeEntity> FetchAlbumType(string name);
        Task<IEnumerable<AlbumEntity>> ListAlbums();
        Task<IEnumerable<AlbumEntity>> ListAlbums(AlbumTypeEntity albumTypeEntity);
    }

    [ServiceImplementation]
    private class DatabaseAdapter : IDatabasePort
    {
        private readonly MyDbContext _dbContext;

        public DatabaseAdapter(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<AlbumTypeEntity> FetchAlbumType(string name)
        {
            return _dbContext.AlbumTypes.SingleAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<AlbumEntity>> ListAlbums()
        {
            return await _dbContext.Albums
                .Include(x => x.AlbumType)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlbumEntity>> ListAlbums(AlbumTypeEntity albumTypeEntity)
        {
            return await _dbContext.Albums
                .Include(x => x.AlbumType)
                .Where(x => x.AlbumType == albumTypeEntity)
                .ToListAsync();
        }
    }

    internal class Handler : IRequestHandler<ListAlbumsQuery, IEnumerable<ResponseItemDto>>
    {
        private readonly IDatabasePort _databaseAdapter;

        public Handler(IDatabasePort databaseAdapter)
        {
            _databaseAdapter = databaseAdapter;
        }

        public async Task<IEnumerable<ResponseItemDto>> Handle(ListAlbumsQuery request, CancellationToken cancellationToken)
        {
            if (request.AlbumType == null)
            {
                var entities = await _databaseAdapter.ListAlbums();
                return entities.Select(EntityToDto);
            }
            else
            {
                var albumType = request.AlbumType.ToString()!;
                var albumTypeEntity = await _databaseAdapter.FetchAlbumType(albumType);
                var entities = await _databaseAdapter.ListAlbums(albumTypeEntity);
                return entities.Select(EntityToDto);
            }
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
    }
}
