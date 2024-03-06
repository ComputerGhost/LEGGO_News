using Core.Domain.FileStorage.Ports;
using Core.Infrastructure.Startup;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure;

[ServiceImplementation]
internal class FilesRepository : IFilesRepository
{
    private readonly string _connectionString = null!;

    public FilesRepository(IOptions<InfrastructureOptions> options)
    {
        _connectionString = options.Value.DatabaseConnectionString;
    }

    public async Task<int> Insert(string fileName)
    {
        var sql = """
            INSERT INTO Files (FileName)
            OUTPUT INSERTED.Id
            VALUES (@fileName)
            """;
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleAsync<int>(sql, new
        {
            fileName, 
        });
    }
}
