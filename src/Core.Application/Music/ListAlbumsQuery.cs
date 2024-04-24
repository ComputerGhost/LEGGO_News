using Core.Domain.Common.Entities;
using Core.Domain.Music.Enums;
using Core.Domain.Music.Ports;
using MediatR;
using static Core.Application.Music.ListAlbumsQuery;

namespace Core.Application.Music;
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

    internal class Handler : IRequestHandler<ListAlbumsQuery, IEnumerable<ResponseItemDto>>
    {
        private readonly IMusicDatabasePort _databaseAdapter;

        public Handler(IMusicDatabasePort databaseAdapter)
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
