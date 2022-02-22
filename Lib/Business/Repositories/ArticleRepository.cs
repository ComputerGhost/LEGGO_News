using AutoMapper;
using Business.DTOs;
using Business.Repositories.Interfaces;
using Data;
using Data.Models;
using System.Linq;

namespace Business.Repositories
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
            _databaseContext.Article.Add(newArticle);
            _databaseContext.SaveChanges();

            return _mapper.Map<ArticleSummary>(newArticle);
        }

        public ArticleDetails Fetch(long id)
        {
            var article = _databaseContext.Article.Find(id);
            if (article == null)
            {
                return null;
            }
            return _mapper.Map<ArticleDetails>(article);
        }

        public SearchResults<ArticleSummary> Search(SearchParameters parameters)
        {
            var foundArticles = _databaseContext.Article;

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
    }
}
