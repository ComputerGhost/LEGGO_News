using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Data;

namespace DataAccess.Repositories
{
    public class ArticlesRepository : 
        IFetcher<ArticleDetails>,
        ISearchable<ArticleSummary>
    {
        private readonly IDbConnection _connection;

        public ArticlesRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // IFetchable
        string IFetcher<ArticleDetails>.FetchSql => 
            "SELECT * FROM Articles WHERE Id = @id";

        // IRepository
        IDbConnection IRepository.Connection => _connection;

        // ISearchable
        string ISearchable<ArticleSummary>.SearchSql => $@"
            SELECT TOP (@limit) Id, Title FROM Articles
            WHERE Id < @start
              AND Title LIKE '%' + @search + '%'
            ORDER BY Id DESC";
        string ISearchable<ArticleSummary>.PointerFunc(ArticleSummary entry)
            => entry.Id.ToString();
    }
}
