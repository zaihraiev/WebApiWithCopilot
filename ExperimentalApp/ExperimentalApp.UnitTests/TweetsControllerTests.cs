using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.Controllers;
using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ExperimentalApp.UnitTests
{
    [TestFixture]
    public class TweetsControllerTests
    {
        private TweetsController _controller;
        private Mock<ITweetService> _tweetServiceMock;

        [SetUp]
        public void Setup()
        {
            _tweetServiceMock = new Mock<ITweetService>();
            _controller = new TweetsController(_tweetServiceMock.Object);
        }

        [Test]
        public void CreateTweet_WhenModelStateIsInvalid_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = _controller.CreateTweet(new TweetCreateViewModel());

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void CreateTweet_WhenModelStateIsValid_ReturnsOk()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.AddTweetAsync(It.IsAny<TweetCreateViewModel>())).Returns(Task.FromResult(true));

            // Act
            var result = _controller.CreateTweet(new TweetCreateViewModel());

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void CreateTweet_WhenAddTweetReturnsFalse_ReturnsBadRequest()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.AddTweetAsync(It.IsAny<TweetCreateViewModel>())).Returns(Task.FromResult(false));

            // Act
            var result = _controller.CreateTweet(new TweetCreateViewModel());

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void GetTweets_WhenTweetsIsEmpty_ReturnsNotFound()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.GetTweetsAsync()).Returns(Task.FromResult(Enumerable.Empty<Tweet>()));

            // Act
            var result = _controller.GetTweets();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GetTweets_WhenTweetsIsNotEmpty_ReturnsOk()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.GetTweetsAsync()).Returns(Task.FromResult(new List<Tweet> { new Tweet() }.AsEnumerable()));

            // Act
            var result = _controller.GetTweets();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetTweetByIdAsync_WhenTweetIsNull_ReturnsNotFound()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Tweet>(null));

            // Act
            var result = _controller.GetTweetByIdAsync("1");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GetTweetByIdAsync_WhenTweetIsNotNull_ReturnsOk()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new Tweet()));

            // Act
            var result = _controller.GetTweetByIdAsync("1");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetFilteredTweets_WhenTweetsIsNotNull_ReturnsOk()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.GetFilteredTweetsAsync(It.IsAny<TweetFilterViewModel>())).Returns(Task.FromResult(new List<Tweet> { new Tweet() }.AsEnumerable()));

            // Act
            var result = _controller.GetFilteredTweets(new TweetFilterViewModel());

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void UpdateTweetById_WhenTweetUpdateFailed_ReturnsBadRequest()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.UpdateTweetById(It.IsAny<string>(), It.IsAny<TweetUpdateViewModel>())).Returns(Task.FromResult(false));

            // Act
            var result = _controller.UpdateTweetById("1", new TweetUpdateViewModel());

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void UpdateTweetById_WhenModelStateIsInvalid_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = _controller.UpdateTweetById("1", new TweetUpdateViewModel());

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void UpdateTweetById_WhenModelStateIsValid_ReturnsOk()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.UpdateTweetById(It.IsAny<string>(), It.IsAny<TweetUpdateViewModel>())).Returns(Task.FromResult(true));

            // Act
            var result = _controller.UpdateTweetById("1", new TweetUpdateViewModel());

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void DeleteTweetByIdAsync_WhenTweetDeletingFailed_ReturnsBadRequest()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.DeleteTweetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(false));

            // Act
            var result = _controller.DeleteTweetByIdAsync("1");

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void DeleteTweetByIdAsync_WhenTweetIsFound_ReturnsOk()
        {
            // Arrange
            _tweetServiceMock.Setup(x => x.DeleteTweetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(true));

            // Act
            var result = _controller.DeleteTweetByIdAsync("1");

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }
    }
}
