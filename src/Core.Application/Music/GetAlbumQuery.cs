using Core.Application.Common.Exceptions;
using Core.Domain.Music.Enums;
using Core.Domain.Music.Ports;
using MediatR;
using static Core.Application.Music.GetAlbumQuery;

namespace Core.Application.Music;
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

    internal class Handler : IRequestHandler<GetAlbumQuery, ResponseDto>
    {
        private readonly IMusicDatabasePort _databaseAdapter;

        public Handler(IMusicDatabasePort databaseAdapter)
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
