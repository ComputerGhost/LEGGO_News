using ArticleTranslator;
using AutoMapper;
using Database.DTOs;
using Database.Repositories.Interfaces;
using System.Text.RegularExpressions;
using Web.Services.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public ArticleService(
            IArticleRepository articleRepository,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }


        public ArticleIndexViewModel Search(SearchParameters parameters)
        {
            var articles = _articleRepository.Search(parameters);

            var viewModel = _mapper.Map<ArticleIndexViewModel>(articles);
            foreach (var datum in viewModel.Data)
            {
                datum.FriendlyUrlSegment = GetFriendlyUrl(datum.Title);
            }

            return viewModel;
        }

        public ArticleViewModel GetArticle(long id)
        {
            var article = _articleRepository.Fetch(id);

            var viewModel = _mapper.Map<ArticleViewModel>(article);
            viewModel.FriendlyUrlSegment = GetFriendlyUrl(article.Title);

            return viewModel;
        }

        /// <summary>
        /// Modifies VM so that Content is in raw HTML.
        /// </summary>
        /// <remarks>
        /// This can be expensive, so call this after validating article.
        /// </remarks>
        public void TranslateToHtml(ArticleViewModel source)
        {
            var factory = new ArticleTranslatorFactory();
            var translator = factory.CreateTranslator(source.Format);
            source.Content = translator.TranslateToHtml(source.Content);
        }


        private string GetFriendlyUrl(string title)
        {
            var lowered = title.ToLower();
            var kebab = Regex.Replace(lowered, @"\s", "-");
            var strippedSpecials = Regex.Replace(kebab, @"[^\w-]", "");
            return strippedSpecials;
        }

    }
}
