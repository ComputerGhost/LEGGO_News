using ContentCore.Models;

namespace ContentCore.Events
{
    public class ArticleListingEventArgs : EventArgs
    {
        /// <summary>
        /// Article summary models being built to return.
        /// </summary>
        public IEnumerable<IArticleSummaryModel> SummaryModels { get; set; } = null!;

        public ArticleListingEventArgs(IEnumerable<IArticleSummaryModel> summaryModels)
        {
            SummaryModels = summaryModels;
        }
    }
}
