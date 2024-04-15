using Core.Common;
using Core.Common.Database;
using Core.Startup;
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

    internal interface IDatabasePort
    {
        Task<AlbumEntity?> FetchAlbum(int id);
    }

    [ServiceImplementation]
    private class DatabaseAdapter : IDatabasePort
    {
        private readonly MyDbContext _dbContext;

        public DatabaseAdapter(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<AlbumEntity?> FetchAlbum(int id)
        {
            return _dbContext.Albums
                .Include(album => album.AlbumType)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }

    public class ResponseDto
    {
        public int Id { get; set; }

        public AlbumType AlbumType { get; set; }

        public string Title { get; set; } = null!;

        public string Artist { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        public int AlbumArtImageId { get; set; }
    }

    internal class Handler : IRequestHandler<GetAlbumQuery, ResponseDto>
    {
        private readonly IDatabasePort _databaseAdapter;

        public Handler(IDatabasePort databaseAdapter)
        {
            _databaseAdapter = databaseAdapter;
        }

        public async Task<ResponseDto> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
        {
            var albumEntity = await _databaseAdapter.FetchAlbum(request.Id);
            if (albumEntity == null)
            {
                throw new NotFoundException();
            }

            return new ResponseDto
            {
                Id = albumEntity.Id,
                AlbumType = Enum.Parse<AlbumType>(albumEntity.AlbumType.Name),
                Title = albumEntity.Title,
                Artist = albumEntity.Artist,
                ReleaseDate = albumEntity.ReleaseDate,
                AlbumArtImageId = albumEntity.ImageId,
            };
        }
    }
}
