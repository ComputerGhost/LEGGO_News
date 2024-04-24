using Core.Domain.Common.Entities;
using Core.Domain.Music.Ports;
using Core.Domain.Startup;
using Core.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Core.Infrastructure.Adapters;

[ServiceImplementation]
internal class MusicDatabaseAdapter : IMusicDatabasePort
{
    private readonly MyDbContext _dbContext;

    public MusicDatabaseAdapter(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Create(AlbumEntity albumEntity)
    {
        _dbContext.Add(albumEntity);
        await _dbContext.SaveChangesAsync();
        return albumEntity.Id;
    }

    public Task DeleteAlbum(int id)
    {
        return _dbContext.Albums.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public Task<AlbumEntity?> FetchAlbum(int id)
    {
        return _dbContext.Albums
            .Include(album => album.AlbumType)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<AlbumTypeEntity> FetchAlbumType(string name)
    {
        return _dbContext.AlbumTypes.SingleAsync(x => x.Name == name);
    }

    public async Task<IEnumerable<AlbumEntity>> ListAlbums()
    {
        return await _dbContext.Albums
            .Include(x => x.AlbumType)
            .ToListAsync();
    }

    public async Task<IEnumerable<AlbumEntity>> ListAlbums(AlbumTypeEntity albumTypeEntity)
    {
        return await _dbContext.Albums
            .Include(x => x.AlbumType)
            .Where(x => x.AlbumType == albumTypeEntity)
            .ToListAsync();
    }

    public Task Update(AlbumEntity entity)
    {
        Debug.Assert(_dbContext.Entry(entity).State == EntityState.Modified);

        return _dbContext.SaveChangesAsync();
    }
}
