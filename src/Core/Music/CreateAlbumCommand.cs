using Core.ImageStorage;
using Core.Music.Repositories;
using MediatR;

namespace Core.Music;
public class CreateAlbumCommand : IRequest<int>
{
    public AlbumType AlbumType { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public string AlbumArtFileName { get; set; } = null!;

    public Stream AlbumArtStream { get; set; } = null!;

    internal class CommandHandler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly IAlbumsRepository _albumsRepository;
        private readonly IImageSaver _imageSaver;

        public CommandHandler(IAlbumsRepository albumsRepository, IImageSaver imageSaver)
        {
            _albumsRepository = albumsRepository;
            _imageSaver = imageSaver;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var albumArtId = await SaveAlbumArt(request);
            return await SaveAlbum(albumArtId, request);
        }

        private Task<int> SaveAlbumArt(CreateAlbumCommand request)
        {
            return _imageSaver.Create(request.AlbumArtFileName, request.AlbumArtStream);
        }

        private async Task<int> SaveAlbum(int albumArtId, CreateAlbumCommand request)
        {
            var albumTypeId = await _albumsRepository.GetAlbumTypeId(request.AlbumType.ToString());
            return await _albumsRepository.Insert(albumTypeId, albumArtId, request.Title, request.Artist, request.ReleaseDate);
        }
    }
}
