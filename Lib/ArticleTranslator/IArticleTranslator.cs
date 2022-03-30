namespace ArticleTranslator
{
    public interface IArticleTranslator
    {
        string GetTranslatorType();

        string TranslateToHtml(string savedData);
    }
}
