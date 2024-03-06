namespace Core.Domain.Ports;
public interface IAlbumsRepository
{
    public Task<int> GetAlbumTypeId(string albumType);

    public Task<int> Insert(int albumTypeId, int albumArtImageId, string title, string artist, DateOnly releaseDate);
}
