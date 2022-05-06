using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleTranslator.Tests.EditorJs
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
            // arrange
            const string json = "[]";

            // act
            var html = Translator.TranslateToHtml(json);
            
            // assert
            Assert.AreEqual("", html);
        }

        [TestMethod]
        public void TranslateToHtml_WithMixedBlockTypes_ReturnsMixedTags()
        {
            // arrange
            const string json = @"[
                {'type': 'header', 'data': {'text' : 'header content', 'level': 1}},
                {'type': 'paragraph', 'data': {'text' : 'paragraph content'}}
            ]";

            // act
            var html = Translator.TranslateToHtml(json);

            // assert
            var expectedHtml = "<h1>header content</h1><p>paragraph content</p>";
            Assert.AreEqual(expectedHtml, html);
        }

        [TestMethod]
        public void TranslateToHtml_WithInvalidTags_RemovesTags()
        {
            // arrange
            const string json = @"[
                {'type': 'header', 'data': {'text' : '<header/>', 'level': 1}},
                {'type': 'paragraph', 'data': {'text' : '<paragraph/>'}}
            ]";

            // act
            var html = Translator.TranslateToHtml(json);

            // assert
            var expectedHtml = "<h1></h1><p></p>";
            Assert.AreEqual(expectedHtml, html);
        }

        [TestMethod]
        public void TranslateToHtml_WithFormatTags_IncludesFormatTags()
        {
            // arrange
            const string json = @"[
                {'type': 'paragraph', 'data': {'text' : '<b>bold</b><em>emphasis</em>'}}
            ]";

            // act
            var html = Translator.TranslateToHtml(json);

            // assert
            var expectedHtml = "<p><b>bold</b><em>emphasis</em></p>";
            Assert.AreEqual(expectedHtml, html);
        }

        [TestMethod]
        public void TranslateToHtml_WithHeaders_ReturnsHnTags()
        {
            // arrange
            const string json = @"[
                {'type': 'header', 'data': {'text' : 'header 1', 'level': 1}},
                {'type': 'header', 'data': {'text' : 'header 2', 'level': 2}},
            ]";

            // act
            var html = Translator.TranslateToHtml(json);

            // assert
            var expectedHtml = "<h1>header 1</h1><h2>header 2</h2>";
            Assert.AreEqual(expectedHtml, html);
        }

        [TestMethod]
        public void TranslateToHtml_WithParagraphs_ReturnsPTags()
        {
            // arrange
            const string json = @"[
                {'type': 'paragraph', 'data': {'text' : 'paragraph content'}}
            ]";

            // act
            var html = Translator.TranslateToHtml(json);

            // assert
            var expectedHtml = "<p>paragraph content</p>";
            Assert.AreEqual(expectedHtml, html);
        }

        [TestMethod]
        public void TranslateToHtml_WithLists_ReturnsListTags()
        {
            // arrange
            const string json = @"[
                {'type': 'list', 'data': {'style': 'ordered', 'items': ['one', 'two']}},
                {'type': 'list', 'data': {'style': 'unordered', 'items': ['one', 'two']}}
            ]";

            // act
            var html = Translator.TranslateToHtml(json);

            // assert
            var expectedHtml = "<ol><li>one</li><li>two</li></ol><ul><li>one</li><li>two</li></ul>";
            Assert.AreEqual(expectedHtml, html);
        }

        [TestMethod]
        public void TranslateToHtml_WithQuotes_ReturnsQuotesTags()
        {
            // arrange
            const string json = @"[
                {'type': 'quote', 'data': {'text' : 'content text', 'caption': 'caption text'}}
            ]";

            // act
            var html = Translator.TranslateToHtml(json);

            // assert
            var expectedHtml = "<figure><blockquote>content text</blockquote><figcaption>caption text</figcaption></figure>";
            Assert.AreEqual(expectedHtml, html);
        }

    }
}
