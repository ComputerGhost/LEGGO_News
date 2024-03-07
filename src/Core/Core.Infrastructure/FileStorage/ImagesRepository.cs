using Core.Domain.FileStorage.Ports;
using Core.Infrastructure.Startup;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.FileStorage;

[ServiceImplementation]
internal class ImagesRepository : IImagesRepository
{
    private readonly string _connectionString = null!;

    public ImagesRepository(IOptions<InfrastructureOptions> options)
    {
        _connectionString = options.Value.DatabaseConnectionString;
    }

    public async Task<int> Insert(int originalFileId, int? largeFileId, int? mediumFileId, int thumbnailFileId)
    {
        var sql = """
            INSERT INTO Images (OriginalFileId, LargeFileId, MediumFileId, ThumbnailFileId)
            OUTPUT INSERTED.Id
            VALUES (@originalFileId, @largeFileId, @mediumFileId, @thumbnailFileId)
            """;
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleAsync<int>(sql, new
        {
            originalFileId,
            largeFileId,
            mediumFileId,
            thumbnailFileId,
        });
    }
}
