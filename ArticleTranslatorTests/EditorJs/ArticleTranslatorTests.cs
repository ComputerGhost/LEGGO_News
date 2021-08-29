using ArticleTranslator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArticleTranslatorTests.EditorJs
{
    [TestClass]
    public class ArticleTranslatorTests
    {
        private IArticleTranslator Translator { get; }

        public ArticleTranslatorTests()
        {
            var factory = new ArticleTranslatorFactory();
            Translator = factory.CreateTranslator("editorjs");
        }


        [TestMethod]
        public void TranslateToHtml_WithEmptyDoc_ReturnsEmptyHtml()
        {
            Assert.AreEqual("", Translator.TranslateToHtml("[]"));
        }

        [TestMethod]
        public void TranslateToHtml_WithMixedBlockTypes_ReturnsMixedTags()
        {
            var json = @"[
                {'type': 'header', 'data': {'text' : 'header content', 'level': 1}},
                {'type': 'paragraph', 'data': {'text' : 'paragraph content'}}
            ]";
            var expectedHtml = "<h1>header content</h1><p>paragraph content</p>";
            Assert.AreEqual(expectedHtml, Translator.TranslateToHtml(json));
        }

        [TestMethod]
        public void TranslateToHtml_WithSpecialCharacters_EscapesCharacters()
        {
            var json = @"[
                {'type': 'header', 'data': {'text' : '<header>', 'level': 1}},
                {'type': 'paragraph', 'data': {'text' : '<paragraph>'}}
            ]";
            var expectedHtml = "<h1>&lt;header&gt;</h1><p>&lt;paragraph&gt;</p>";
            Assert.AreEqual(expectedHtml, Translator.TranslateToHtml(json));
        }

        [TestMethod]
        public void TranslateToHtml_WithHeaders_ReturnsHnTags()
        {
            var json = @"[
                {'type': 'header', 'data': {'text' : 'header 1', 'level': 1}},
                {'type': 'header', 'data': {'text' : 'header 2', 'level': 2}},
            ]";
            var expectedHtml = "<h1>header 1</h1><h2>header 2</h2>";
            Assert.AreEqual(expectedHtml, Translator.TranslateToHtml(json));
        }

        [TestMethod]
        public void TranslateToHtml_WithParagraphs_ReturnsPTags()
        {
            var json = @"[
                {'type': 'paragraph', 'data': {'text' : 'paragraph content'}}
            ]";
            var expectedHtml = "<p>paragraph content</p>";
            Assert.AreEqual(expectedHtml, Translator.TranslateToHtml(json));
        }
    }
}
