using Database.DTOs;

namespace Public.ViewModels
{
    public class ArticleViewModel : ArticleDetails
    {
        public string FriendlyUrlSegment { get; set; }
    }
}
