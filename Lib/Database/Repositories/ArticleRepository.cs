using AutoMapper;
using Database.DTOs;
using Database.Internal;
using Database.Internal.Models;
using Database.Repositories.Interfaces;
using System.Linq;

namespace Database.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public ArticleRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public ArticleSummary Create(ArticleSaveData saveData)
        {
            var newArticle = _mapper.Map<Article>(saveData);
            _databaseContext.Articles.Add(newArticle);
            _databaseContext.SaveChanges();

            return _mapper.Map<ArticleSummary>(newArticle);
        }

        public void Delete(long id)
        {
            var article = _databaseContext.Articles.Find(id);
            _databaseContext.Articles.Remove(article);
            _databaseContext.SaveChanges();
        }

        public ArticleDetails Fetch(long id)
        {
            var article = _databaseContext.Articles.Find(id);
            if (article == null)
            {
                return null;
            }
            return _mapper.Map<ArticleDetails>(article);
        }

        public SearchResults<ArticleSummary> Search(SearchParameters parameters)
        {
            var foundArticles = _databaseContext.Articles.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Query))
            {
                foundArticles = foundArticles.Where(article => article.Title.Contains(parameters.Query));
            }

            var articlesPage = foundArticles
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return new SearchResults<ArticleSummary>
            {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = articlesPage.Count(),
                TotalCount = foundArticles.Count(),
                Data = articlesPage.Select(article => _mapper.Map<ArticleSummary>(article))
            };
        }

        public void Update(long id, ArticleSaveData saveData)
        {
            var article = _databaseContext.Articles.Find(id);
            _mapper.Map(saveData, article);
            _databaseContext.SaveChanges();
        }
    }
}
