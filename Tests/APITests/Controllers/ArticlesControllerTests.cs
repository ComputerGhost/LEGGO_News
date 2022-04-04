﻿using API.Controllers;
using Business.DTOs;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APITests.Controllers
{
    [TestClass]
    public class ArticlesControllerTests
    {
        private Mock<IArticleRepository> _mockArticleRepository;
        private ArticlesController _articlesController;

        [TestInitialize]
        public void Initialize()
        {
            _mockArticleRepository = new Mock<IArticleRepository>();
            _articlesController = new ArticlesController(_mockArticleRepository.Object);
        }

        [TestMethod]
        public void List_Always_CallsRepositoryWithSearchParameters()
        {
            // act
            var parameters = new SearchParameters();
            _articlesController.List(parameters);

            // assert
            _mockArticleRepository.Verify(r => r.Search(It.Is<SearchParameters>(p => p == parameters)));
        }

        [TestMethod]
        public void List_Always_ReturnsSearchResults()
        {
            // arrange
            const int REQUEST_KEY = 123;
            var expectedResults = new SearchResults<ArticleSummary>
            {
                Key = REQUEST_KEY
            };
            _mockArticleRepository.Setup(r => r.Search(It.IsAny<SearchParameters>())).Returns(expectedResults);

            // act
            var parameters = new SearchParameters();
            var result = _articlesController.List(parameters);

            // assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            Assert.IsInstanceOfType(jsonResult.Value, typeof(SearchResults<ArticleSummary>));
            var actualResults = (SearchResults<ArticleSummary>)jsonResult.Value;
            Assert.AreEqual(actualResults.Key, REQUEST_KEY);
        }

        [TestMethod]
        public void Get_Always_CallsRepositoryWithId()
        {
            // arrange
            const int ARTICLE_ID = 123;

            // act
            _articlesController.Get(ARTICLE_ID);

            // assert
            _mockArticleRepository.Verify(r => r.Fetch(It.Is<long>(p => p == ARTICLE_ID)));
        }

        [TestMethod]
        public void Get_WhenFound_ReturnsArticleDetails()
        {
            // arrange
            const int ARTICLE_ID = 123;
            var expectedDetails = new ArticleDetails
            {
                Id = ARTICLE_ID
            };
            _mockArticleRepository.Setup(r => r.Fetch(It.IsAny<long>())).Returns(expectedDetails);

            // act
            var result = _articlesController.Get(ARTICLE_ID);

            // assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            Assert.IsInstanceOfType(jsonResult.Value, typeof(ArticleDetails));
            var actualDetails = (ArticleDetails)jsonResult.Value;
            Assert.AreEqual(ARTICLE_ID, actualDetails.Id);
        }

        [TestMethod]
        public void Get_WhenNotFound_ReturnsNotFound()
        {
            // arrange
            _mockArticleRepository.Setup(r => r.Fetch(It.IsAny<long>())).Returns<ArticleDetails>(null);

            // act
            var result = _articlesController.Get(0);

            // assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Create_Always_CallsRepositoryWithData()
        {
            // arrange
            const string ARTICLE_TITLE = "article title";
            var incomingSaveData = new ArticleSaveData
            {
                Title = ARTICLE_TITLE
            };
            var createdSummary = new ArticleSummary();
            _mockArticleRepository.Setup(r => r.Create(It.IsAny<ArticleSaveData>())).Returns(createdSummary);

            // act
            _articlesController.Create(incomingSaveData);

            // assert
            _mockArticleRepository.Verify(r => r.Create(It.Is<ArticleSaveData>(sd => sd.Title == ARTICLE_TITLE)));
        }

        [TestMethod]
        public void Create_Always_ReturnsCreatedResult()
        {
            // arrange
            const int ARTICLE_ID = 123;
            var createdSummary = new ArticleSummary
            {
                Id = ARTICLE_ID
            };
            _mockArticleRepository.Setup(r => r.Create(It.IsAny<ArticleSaveData>())).Returns(createdSummary);

            // act
            var saveData = new ArticleSaveData();
            var result = _articlesController.Create(saveData);

            // assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = (CreatedAtActionResult)result;
            Assert.IsInstanceOfType(createdResult.Value, typeof(ArticleSummary));
            var actualSummary = (ArticleSummary)createdResult.Value;
            Assert.AreEqual(ARTICLE_ID, actualSummary.Id);
        }
    }
}
