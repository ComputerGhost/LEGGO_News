using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SearchResults<T>
    {
        /// <summary>
        /// Matches the Key value in the search request.
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// The number of records skipped before the returned results.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// The number of records returned in the response.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The total number of records.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// An enumeration of the returned data.
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
