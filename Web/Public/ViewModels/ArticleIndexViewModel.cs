using APIClient.DTOs;

namespace Public.ViewModels
{
    public class ArticleIndexViewModel : SearchResults<ArticleIndexViewModel.ArticleIndexItem>
    {
        public class ArticleIndexItem : ArticleSummary
        {
            public string FriendlyUrlSegment { get; set; }
        }
    }
}
