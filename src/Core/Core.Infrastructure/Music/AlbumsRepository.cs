using Core.Domain.Ports;
using Core.Infrastructure.Startup;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.Music;
internal class AlbumsRepository : IAlbumsRepository
{
    private readonly string _connectionString = null!;

    public AlbumsRepository(IOptions<InfrastructureOptions> options)
    {
        _connectionString = options.Value.DatabaseConnectionString;
    }

    public async Task<int> GetAlbumTypeId(string albumType)
    {
        var sql = "SELECT Id FROM AlbumTypes WHERE Name = @albumType";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleAsync<int>(sql, new
        {
            albumType,
        });
    }

    public async Task<int> Insert(int albumTypeId, int albumArtImageId, string title, string artist, DateOnly releaseDate)
    {
        var sql = """
            INSERT INTO Albums (AlbumTypeId, ImageId, Title, Artist, ReleaseDate)
            OUTPUT INSERTED.Id
            VALUES (@albumTypeId, @albumArtImageId, @title, @artist, @releaseDate)
            """;
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleAsync<int>(sql, new
        {
            albumTypeId,
            albumArtImageId,
            title,
            artist,
            releaseDate,
        });
    }
}
