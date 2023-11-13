using API.DTOs.Article;
using System.ComponentModel.DataAnnotations;

namespace API.Modules.Articles.DTOs
{
    public class UpdateArticleDto
    {
        public IEnumerable<Block>? Content { get; set; }

        public IEnumerable<string>? Tags { get; set; }

        [StringLength(255)]
        public string? Title { get; set; }
    }
}
