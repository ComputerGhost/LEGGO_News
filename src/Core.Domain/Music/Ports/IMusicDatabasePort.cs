using Core.Domain.Common.Entities;

namespace Core.Domain.Music.Ports;
public interface IMusicDatabasePort
{
    Task<int> Create(AlbumEntity albumEntity);

    Task DeleteAlbum(int id);

    Task<AlbumEntity?> FetchAlbum(int id);

    Task<AlbumTypeEntity> FetchAlbumType(string name);

    Task<IEnumerable<AlbumEntity>> ListAlbums();

    Task<IEnumerable<AlbumEntity>> ListAlbums(AlbumTypeEntity albumTypeEntity);

    Task Update(AlbumEntity entity);
}
