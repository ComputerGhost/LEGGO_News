using ArticleTranslator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArticleTranslatorTests
{
    [TestClass]
    public class ArticleTranslatorFactoryTests
    {
        ArticleTranslatorFactory Factory = new ArticleTranslatorFactory();

        string GetTranslatorType(string format)
        {
            var translator = Factory.CreateTranslator(format);
            return translator.GetTranslatorType();
        }


        [TestMethod]
        public void CreateTranslator_WithValidFormat_ReturnsCorrectTranslator()
        {
            Assert.AreEqual("EditorJs", GetTranslatorType("EditorJs"));
        }

        [TestMethod]
        public void CreateTranslator_WithMixedCharacterCase_ReturnsTranslator()
        {
            Assert.AreEqual("EditorJs", GetTranslatorType("editorjs"));
            Assert.AreEqual("EditorJs", GetTranslatorType("EDITORJS"));
        }

        [TestMethod]
        public void CreateTranslator_WithInvalidFormat_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => Factory.CreateTranslator("invalid"), 
                "Format is not supported."
            );
        }
    }
}
