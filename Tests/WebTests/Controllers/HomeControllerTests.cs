using Business.DTOs;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Controllers;

namespace WebTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IArticlesRepository> _mockArticlesRepository;
        private HomeController _homeController;

        [TestInitialize]
        public void Initialize()
        {
            _mockArticlesRepository = new Mock<IArticlesRepository>();
            _homeController = new HomeController(_mockArticlesRepository.Object);
        }

        [TestMethod]
        public void Index_Always_ReturnsArticles()
        {
            // arrange
            const int REQUEST_KEY = 1;
            var expectedResults = new SearchResults<ArticleSummary>
            {
                Key = REQUEST_KEY,
            };
            _mockArticlesRepository.Setup(r => r.Search(It.IsAny<SearchParameters>())).Returns(expectedResults);

            // act
            var result = _homeController.Index();

            // assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(SearchResults<ArticleSummary>));
            var actualResults = (SearchResults<ArticleSummary>)viewResult.Model;
            Assert.AreEqual(actualResults.Key, REQUEST_KEY);
        }
    }
}
