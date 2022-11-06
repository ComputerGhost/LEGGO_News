namespace APIClient.DTOs
{
    public class ArticleDetails
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Format { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
