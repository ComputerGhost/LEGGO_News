using Core.Domain.Ports;

namespace Core.Domain.Music;
public class AlbumWriter
{
    private readonly IAlbumsRepository _albumsRepository;

    public AlbumWriter(IAlbumsRepository albumsRepository)
    {
        _albumsRepository = albumsRepository;
    }

    public async Task<int> CreateAlbum(AlbumType albumType, int albumArtFileId, string title, string artist, DateOnly releaseDate)
    {
        var albumTypeId = await _albumsRepository.GetAlbumTypeId(albumType.ToString());
        return await _albumsRepository.Insert(albumTypeId, albumArtFileId, title, artist, releaseDate);
    }
}
