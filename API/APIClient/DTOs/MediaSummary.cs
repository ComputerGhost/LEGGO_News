﻿namespace APIClient.DTOs
{
    public class MediaSummary
    {
        public long Id { get; set; }

        public string? Caption { get; set; }

        public string ThumbnailUrl { get; set; } = string.Empty;
    }
}
