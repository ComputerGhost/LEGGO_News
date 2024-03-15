namespace Core.Music.Repositories;
internal interface IAlbumsRepository
{
    public Task<int> GetAlbumTypeId(string albumType);

    public Task<int> Insert(int albumTypeId, int albumArtImageId, string title, string artist, DateOnly releaseDate);
}
