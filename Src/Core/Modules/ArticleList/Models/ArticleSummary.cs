namespace Core.Modules.ArticleList.Models;

public class ArticleSummary
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Abstract { get; set; } = null!;
}
