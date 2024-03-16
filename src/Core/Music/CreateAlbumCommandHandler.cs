using Core.ImageStorage;
using Core.Music.Repositories;
using MediatR;

namespace Core.Music;
internal class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, int>
{
    private readonly IAlbumsRepository _albumsRepository;
    private readonly IImageSaver _imageSaver;

    public CreateAlbumCommandHandler(IAlbumsRepository albumsRepository, IImageSaver imageSaver)
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
