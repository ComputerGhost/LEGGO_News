using Database.DTOs;

namespace Web.ViewModels
{
    public class ArticleViewModel : ArticleDetails
    {
        public string FriendlyUrlSegment { get; set; }
    }
}
