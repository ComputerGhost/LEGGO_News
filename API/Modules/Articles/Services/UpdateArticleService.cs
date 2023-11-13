using API.Modules.Articles.DTOs;
using Dapper;
using System.Data;

namespace API.Modules.Articles.Services
{
    public class UpdateArticleService
    {
        private readonly IDbConnection _connection;

        public UpdateArticleService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task UpdateArticleAsync(
            int articleId,
            HashSet<string> updatedProperties,
            UpdateArticleDto updateDto)
        {
            var columns = new List<string>();
            var parameters = new DynamicParameters();
        }
    }
}
