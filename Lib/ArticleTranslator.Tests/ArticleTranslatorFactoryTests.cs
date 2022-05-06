using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ArticleTranslator.Tests
{
    [TestClass]
    public class ArticleTranslatorFactoryTests
    {
        private ArticleTranslatorFactory Factory { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Factory = new ArticleTranslatorFactory();
        }


        [DataTestMethod]
        [DataRow("EditorJs")]
        public void CreateTranslator_WithValidFormat_CreatesTranslator(string format)
        {
            // act
            var translator = Factory.CreateTranslator(format);

            // assert
            Assert.AreEqual(format, translator.GetTranslatorType());
        }

        [DataTestMethod]
        [DataRow("EditorJs")]
        public void CreateTranslator_WithValidMixedCaseFormat_CreatesTranslator(string format)
        {
            // arrange
            var lowercase = format.ToLower();
            var uppercase = format.ToUpper();
            var mixedcase = string.Join("", uppercase.Select((c, i) => (i % 2 == 0) ? c : char.ToLower(c)));

            // act
            var lowercaseTranslator = Factory.CreateTranslator(lowercase);
            var uppercaseTranslator = Factory.CreateTranslator(uppercase);
            var mixedcaseTranslator = Factory.CreateTranslator(mixedcase);

            // assert
            Assert.AreEqual(format, lowercaseTranslator.GetTranslatorType());
            Assert.AreEqual(format, uppercaseTranslator.GetTranslatorType());
            Assert.AreEqual(format, mixedcaseTranslator.GetTranslatorType());
        }

        public void CreateTranslator_WithInvalidFormat_ThrowsException()
        {
            // arrange
            const string INVALID = "[invalid translator format]";

            // act (later)
            var createTranslator = () => Factory.CreateTranslator(INVALID);

            // assert
            Assert.ThrowsException<ArgumentException>(
                createTranslator,
                "Format is not supported."
            );
        }
    }
}
