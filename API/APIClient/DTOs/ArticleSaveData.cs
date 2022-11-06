using System.ComponentModel.DataAnnotations;

namespace APIClient.DTOs
{
    public class ArticleSaveData
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Format { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
