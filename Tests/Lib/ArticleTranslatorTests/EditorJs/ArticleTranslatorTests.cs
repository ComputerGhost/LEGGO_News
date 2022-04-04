using ArticleTranslator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArticleTranslatorTests.EditorJs
{
    [TestClass]
    public class ArticleTranslatorTests
    {
        private IArticleTranslator Translator { get; set; }

        [TestInitialize]
        public void Initialize()
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
        public void TranslateToHtml_WithInvalidTags_RemovesTags()
        {
            var json = @"[
                {'type': 'header', 'data': {'text' : '<header/>', 'level': 1}},
                {'type': 'paragraph', 'data': {'text' : '<paragraph/>'}}
            ]";
            var expectedHtml = "<h1></h1><p></p>";
            Assert.AreEqual(expectedHtml, Translator.TranslateToHtml(json));
        }

        [TestMethod]
        public void TranslateToHtml_WithFormatTags_IncludesFormatTags()
        {
            var json = @"[
                {'type': 'paragraph', 'data': {'text' : '<b>bold</b><em>emphasis</em>'}}
            ]";
            var expectedHtml = "<p><b>bold</b><em>emphasis</em></p>";
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

        [TestMethod]
        public void TranslateToHtml_WithLists_ReturnsListTags()
        {
            var json = @"[
                {'type': 'list', 'data': {'style': 'ordered', 'items': ['one', 'two']}},
                {'type': 'list', 'data': {'style': 'unordered', 'items': ['one', 'two']}}
            ]";
            var expectedHtml = "<ol><li>one</li><li>two</li></ol><ul><li>one</li><li>two</li></ul>";
            Assert.AreEqual(expectedHtml, Translator.TranslateToHtml(json));
        }

        [TestMethod]
        public void TranslateToHtml_WithQuotes_ReturnsQuotesTags()
        {
            var json = @"[
                {'type': 'quote', 'data': {'text' : 'content text', 'caption': 'caption text'}}
            ]";
            var expectedHtml = "<figure><blockquote>content text</blockquote><figcaption>caption text</figcaption></figure>";
            Assert.AreEqual(expectedHtml, Translator.TranslateToHtml(json));
        }
    }
}
