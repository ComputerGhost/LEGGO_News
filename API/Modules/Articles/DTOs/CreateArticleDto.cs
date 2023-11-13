using API.DTOs.Article;
using System.ComponentModel.DataAnnotations;

namespace API.Modules.Articles.DTOs
{
    public class CreateArticleDto
    {
        [Required]
        public IEnumerable<Block> Content { get; set; } = null!;

        public IEnumerable<string>? Tags { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;
    }
}
