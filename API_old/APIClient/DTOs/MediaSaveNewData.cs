namespace APIClient.DTOs
{
    public class MediaSaveNewData
    {
        public string MimeType { get; set; } = string.Empty;

        public string LocalFilename { get; set; } = string.Empty;

        public string OriginalFilename { get; set; } = string.Empty;

        public string LargestResize { get; set; } = string.Empty;
    }
}
