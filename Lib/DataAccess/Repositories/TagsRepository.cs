using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Data;

namespace DataAccess.Repositories
{
    public class TagsRepository :
        ICreator<TagSaveData>,
        IDeleter,
        IFetcher<TagDetails>,
        ISearchable<TagSummary>,
        IUpdater<TagSaveData>
    {
        private readonly IDbConnection _connection;

        public TagsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // ICreator
        string ICreator<TagSaveData>.InsertSql => @"
            INSERT INTO Tags (Name)
            OUTPUT INSERTED.Id
            VALUES (@Name)";

        // IDeleter
        string IDeleter.DeleteSql => 
            "DELETE FROM Tags WHERE Id = @id";

        // IFetchable
        string IFetcher<TagDetails>.FetchSql => 
            "SELECT * FROM Tags WHERE Id = @Id";

        // IRepository
        IDbConnection IRepository.Connection => _connection;

        // ISearchable
        string ISearchable<TagSummary>.SearchSql => @"
            SELECT TOP (@limit) Id, Name FROM Tags 
            WHERE Name >= @start 
              AND Name LIKE '%' + @search + '%'
            ORDER BY Name";
        string ISearchable<TagSummary>.PointerFunc(TagSummary entry)
            => entry.Name;

        // IUpdater
        string IUpdater<TagSaveData>.UpdateSql => @"
            UPDATE Tags
            SET Name = @Name
            WHERE Id = @Id";
    }
}
