using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SearchParameters
    {
        /// <summary>
        /// The Key value to include in the search response.
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Query string for searching the data.
        /// </summary>
        public string Query { get; set; } = null;

        /// <summary>
        /// The number of records to skip in the search results.
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// The number of records to include in the search results.
        /// </summary>
        public int Count { get; set; } = 10;
    }
}
