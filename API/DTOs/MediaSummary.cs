using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MediaSummary
    {
        /// <summary>
        /// Unique identifier for the image.
        /// </summary>
        public long ImageId { get; set; }

        /// <summary>
        /// Short description or title of the image.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Name of the photographer or creator.
        /// </summary>
        public string Credit { get; set; }

        /// <summary>
        /// URL whence the image came.
        /// </summary>
        public string CreditUrl { get; set; }

        /// <summary>
        /// URL to the unsized, uncropped original image.
        /// </summary>
        public string OriginalUrl { get; set; }
    }
}

/*
 * 
 * media.example.com/image.jpg
 * media.example.com/small/image.jpg
 * media.example.com/medium/image.jpg
 * media.example.com/large/image.jpg
 * media.example.com/thumbnail/image.jpg
 * 
 */