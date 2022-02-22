using API.Controllers;
using Business.DTOs;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APITests.Controllers
{
    [TestClass]
    public class TagsControllerTests
    {
        private Mock<ITagRepository> _mockTagRepository;
        private TagsController _tagsController;

        [TestInitialize]
        public void Initialize()
        {
            _mockTagRepository = new Mock<ITagRepository>();
            _tagsController = new TagsController(_mockTagRepository.Object);
        }

        [TestMethod]
        public void List_Always_CallsRepositoryWithSearchParameters()
        {
            // act
            var parameters = new SearchParameters();
            _tagsController.List(parameters);

            // assert
            _mockTagRepository.Verify(r => r.Search(It.Is<SearchParameters>(p => p == parameters)));
        }

        [TestMethod]
        public void List_Always_ReturnsSearchResults()
        {
            // arrange
            const int REQUEST_KEY = 123;
            var expectedResults = new SearchResults<TagSummary>
            {
                Key = REQUEST_KEY
            };
            _mockTagRepository.Setup(r => r.Search(It.IsAny<SearchParameters>())).Returns(expectedResults);

            // act
            var parameters = new SearchParameters();
            var result = _tagsController.List(parameters);

            // assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            Assert.IsInstanceOfType(jsonResult.Value, typeof(SearchResults<TagSummary>));
            var actualResults = ((SearchResults<TagSummary>)jsonResult.Value);
            Assert.AreEqual(actualResults.Key, REQUEST_KEY);
        }

        [TestMethod]
        public void Get_Always_CallsRepositoryWithId()
        {
            // arrange
            const int TAG_ID = 123;

            // act
            _tagsController.Get(TAG_ID);

            // assert
            _mockTagRepository.Verify(r => r.Fetch(It.Is<long>(p => p == TAG_ID)));
        }

        [TestMethod]
        public void Get_WhenFound_ReturnsTagDetails()
        {
            // arrange
            const int TAG_ID = 123;
            var expectedDetails = new TagDetails
            {
                Id = TAG_ID
            };
            _mockTagRepository.Setup(r => r.Fetch(It.IsAny<long>())).Returns(expectedDetails);

            // act
            var result = _tagsController.Get(TAG_ID);

            // assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            Assert.IsInstanceOfType(jsonResult.Value, typeof(TagDetails));
            var actualDetails = (TagDetails)jsonResult.Value;
            Assert.AreEqual(TAG_ID, actualDetails.Id);
        }

        [TestMethod]
        public void Get_WhenNotFound_ReturnsNotFound()
        {
            // arrange
            _mockTagRepository.Setup(r => r.Fetch(It.IsAny<long>())).Returns<TagDetails>(null);

            // act
            var result = _tagsController.Get(0);

            // assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Create_Always_CallsRepositoryWithData()
        {
            // arrange
            const string TAG_NAME = "tag name";
            var incomingData = new TagSaveData
            {
                Name = TAG_NAME
            };
            var createdSummary = new TagSummary();
            _mockTagRepository.Setup(r => r.Create(It.IsAny<TagSaveData>())).Returns(createdSummary);

            // act
            _tagsController.Create(incomingData);

            // assert
            _mockTagRepository.Verify(r => r.Create(It.Is<TagSaveData>(sd => sd.Name == TAG_NAME)));
        }

        [TestMethod]
        public void Create_Always_ReturnsCreatedResult()
        {
            // arrange
            const int TAG_ID = 123;
            var createdSummary = new TagSummary
            {
                Id = TAG_ID
            };
            _mockTagRepository.Setup(r => r.Create(It.IsAny<TagSaveData>())).Returns(createdSummary);

            // act
            var saveData = new TagSaveData();
            var result = _tagsController.Create(saveData);

            // assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
            var createdResult = (CreatedResult)result;
            Assert.AreEqual($"./{TAG_ID}", createdResult.Location);
            Assert.IsInstanceOfType(createdResult.Value, typeof(TagSummary));
            var actualSummary = (TagSummary)createdResult.Value;
            Assert.AreEqual(TAG_ID, actualSummary.Id);
        }
    }
}
