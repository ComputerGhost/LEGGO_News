namespace Business.DTOs
{
    public class MediaDetails
    {
        /// <summary>
        /// Unique identifier for the image.
        /// </summary>
        public long Id { get; set; }

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

        public string ThumbnailUrl { get; set; }

        public string SmallSizeUrl { get; set; }

        public string MediumSizeUrl { get; set; }

        public string LargeSizeUrl { get; set; }
    }
}
