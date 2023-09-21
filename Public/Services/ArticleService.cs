using ArticleTranslator;
using System.Text.RegularExpressions;
using Public.Services.Interfaces;
using Public.ViewModels;
using AutoMapper;

namespace Public.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticlesConnection _articlesConnection;
        private readonly IMapper _mapper;

        public ArticleService(
            IArticlesConnection articlesConnection,
            IMapper mapper)
        {
            _articlesConnection = articlesConnection;
            _mapper = mapper;
        }


        public async Task<ArticleIndexViewModel> SearchAsync(SearchParameters parameters)
        {
            var articles = await _articlesConnection.ListAsync(parameters);

            var viewModel = _mapper.Map<ArticleIndexViewModel>(articles);
            foreach (var datum in viewModel.Data)
            {
                datum.FriendlyUrlSegment = GetFriendlyUrl(datum.Title);
            }

            return viewModel;
        }

        public async Task<ArticleViewModel> GetArticleAsync(long id)
        {
            var article = await _articlesConnection.FetchAsync(id);

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
