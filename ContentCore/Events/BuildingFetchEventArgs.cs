using ContentCore.Articles.Models;

namespace ContentCore.Events
{
    public class BuildingFetchEventArgs : EventArgs
    {
        /// <summary>
        /// Queries-parsers being populated to fetch parts of the article.
        /// </summary>
        public IEnumerable<FetchQuery> FetchQueries { get; set; } = null!;

        internal BuildingFetchEventArgs(IEnumerable<FetchQuery> fetchQueries)
        {
            FetchQueries = fetchQueries;
        }
    }
}
