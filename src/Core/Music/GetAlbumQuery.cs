using Azure.Core;
using Core.Common;
using Core.Common.Database;
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
        public int Id { get; set; }

        public AlbumType AlbumType { get; set; }

        public string Title { get; set; } = null!;

        public string Artist { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        public int AlbumArtImageId { get; set; }
    }

    private class CommandHandler : IRequestHandler<GetAlbumQuery, ResponseDto>
    {
        private readonly MyDbContext _dbContext;

        public CommandHandler(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseDto> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
        {
            var albumEntity = await GetAlbum(request.Id);
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

        private Task<AlbumEntity?> GetAlbum(int id)
        {
            return _dbContext.Albums
                .Include(album => album.AlbumType)
                .SingleOrDefaultAsync(album => album.Id == id);
        }
    }
}
