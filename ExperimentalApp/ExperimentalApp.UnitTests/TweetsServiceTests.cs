using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.BusinessLogic.Services;
using ExperimentalApp.Core.Constants;
using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.Core.ViewModels;
using ExperimentalApp.DataAccessLayer.Interfaces;
using ExperimentalApp.UnitTests.Fakes;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq;
using System.Security.Claims;

namespace ExperimentalApp.UnitTests
{
    [TestFixture]
    public class TweetsServiceTests
    {
        private Mock<ITweetRepository> _tweetRepositoryMock;
        private ITweetService _tweetService;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<FakeUserManager> _mockUserManager;

        [SetUp]
        public void Setup()
        {
            _mockUserManager = new Mock<FakeUserManager>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _tweetRepositoryMock = new Mock<ITweetRepository>();
            _tweetService = new TweetService(_tweetRepositoryMock.Object, _mockUserManager.Object, _httpContextAccessorMock.Object);
        }

        [Test]
        public void AddTweet_WhenUserIsUndefined_ReturnsFalse()
        {
            // Arrange
            SetNullIdentifierToUser();
            _tweetRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(false));

            // Act
            var result = _tweetService.AddTweetAsync(new TweetCreateViewModel());

            // Assert
            Assert.IsFalse(result.Result);
            _tweetRepositoryMock.Verify(mock => mock.AddTweet(It.IsAny<Tweet>()), Times.Never);
        }

        [Test]
        public void AddTweet_WhenUserIsDefined_ReturnsTrue()
        {
            // Arrange
            SetIdentifierToUser("1", RolesConstants.Customer);
            _tweetRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(true));
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());

            // Act
            var result = _tweetService.AddTweetAsync(new TweetCreateViewModel());

            // Assert
            Assert.IsTrue(result.Result);
            _tweetRepositoryMock.Verify(mock => mock.AddTweet(It.IsAny<Tweet>()), Times.Once);
        }

        [Test]
        public async Task GetTweets_WhenTweetsAreNotEmpty_ReturnsNotEmptyList()
        {
            // Arrange
            var initialTweets = GetInitialMockTweets();
            _tweetRepositoryMock.Setup(x => x.GetTweetsAsync()).ReturnsAsync(GetInitialMockTweets());

            // Act
            var result = await _tweetService.GetTweetsAsync();

            // Assert
            Assert.AreEqual(initialTweets.Count, result.Count());
            Assert.IsTrue(initialTweets.Zip(result, (i, r) => i.TweetId == r.TweetId ).All(x => x));
        }

        [Test]
        public void GetTweetById_WhenTweetIsFound_ReturnsTweet()
        {
            // Arrange
            var id = "2";
            _tweetRepositoryMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetInitialMockTweets().FirstOrDefault(x => x.TweetId == id));

            // Act
            var result = _tweetService.GetTweetByIdAsync(id);

            // Assert
            Assert.That(result.Result.TweetId, Is.EqualTo(id));
        }

        [Test]
        public void GetFilteredTweetsAsync_WhenFilterByText_ReturnsListFilteredByText()
        {
            // Arrange
            var textFilter = "Test tweet 2";
            _tweetRepositoryMock.Setup(x => x.GetFilteredTweetsAsync(It.IsAny<TweetFilterViewModel>()))
                .ReturnsAsync(GetInitialMockTweets().Where(x => x.Text.ToLower().Contains(textFilter.ToLower())));

            // Act
            var result = _tweetService.GetFilteredTweetsAsync(new TweetFilterViewModel { Text = textFilter });

            // Assert
            Assert.IsTrue(result.Result.Any());
            Assert.That(result.Result.All(x => x.Text.ToLower().Contains(textFilter.ToLower())));
        }

        [Test]
        public void UpdateTweetById_WhenTweetIsNotFound_ReturnsFalse()
        {
            // Arrange
            var id = "3";
            _tweetRepositoryMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>())).ReturnsAsync((Tweet)null);

            // Act
            var result = _tweetService.UpdateTweetById(id, new TweetUpdateViewModel());

            // Assert
            Assert.IsFalse(result.Result);
            _tweetRepositoryMock.Verify(mock => mock.UpdateTweet(It.IsAny<Tweet>()), Times.Never);
        }

        [Test]
        public void UpdateTweetById_WhenTweetIsFound_UpdateTweetAndReturnsTrue()
        {
            // Arrange
            var id = "2";
            _tweetRepositoryMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>())).ReturnsAsync(GetInitialMockTweets().FirstOrDefault(x => x.TweetId == id));
            _tweetRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(true));

            // Act
            var result = _tweetService.UpdateTweetById(id, new TweetUpdateViewModel());

            // Assert
            Assert.IsTrue(result.Result);
            _tweetRepositoryMock.Verify(mock => mock.UpdateTweet(It.IsAny<Tweet>()), Times.Once);
        }

        [Test]
        public void UpdateTweetById_WhenTweetTextChanged_UpdatesTextWithProvidedTextAndReturnsTrue()
        {
            // Arrange
            var id = "2";
            var tweet = GetInitialMockTweets().FirstOrDefault(x => x.TweetId == id);
            var tweetUpdateViewModel = new TweetUpdateViewModel
            {
                Text = "Test text"
            };
            _tweetRepositoryMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>())).ReturnsAsync(tweet);
            _tweetRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(true));

            // Act
            var result = _tweetService.UpdateTweetById(id, tweetUpdateViewModel);

            // Assert
            Assert.IsTrue(result.Result);
            Assert.That(tweetUpdateViewModel.Text, Is.EqualTo(tweet.Text));
            _tweetRepositoryMock.Verify(mock => mock.UpdateTweet(tweet), Times.Once);
        }

        [Test]
        public void UpdateTweetById_WhenTweetTagsChanged_UpdatesTagsWithProvidedListAndReturnsTrue()
        {
            // Arrange
            var id = "2";
            var tweet = GetInitialMockTweets().FirstOrDefault(x => x.TweetId == id);
            var tweetUpdateViewModel = new TweetUpdateViewModel
            {
                Tags = new List<string>
                {
                    "TestTag1",
                    "TestTag2"
                }
            };
            _tweetRepositoryMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>())).ReturnsAsync(tweet);
            _tweetRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(true));

            // Act
            var result = _tweetService.UpdateTweetById(id, tweetUpdateViewModel);

            // Assert
            Assert.IsTrue(result.Result);
            Assert.IsTrue(tweetUpdateViewModel.Tags.All(t => tweet.Tags.Contains(t)));
            _tweetRepositoryMock.Verify(mock => mock.UpdateTweet(tweet), Times.Once);
        }

        [Test]
        public void DeleteTweetById_WhenTweetIsNotFound_ReturnsFalse()
        {
            // Arrange
            _tweetRepositoryMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Tweet)null);

            // Act
            var result = _tweetService.DeleteTweetByIdAsync("1");

            // Assert
            Assert.IsFalse(result.Result);
            _tweetRepositoryMock.Verify(mock => mock.DeleteTweet(It.IsAny<Tweet>()), Times.Never);
        }

        [Test]
        public void DeleteTweetById_WhenTweetIsFound_ReturnsTrue()
        {
            // Arrange
            var id = "2";
            var initialTweet = GetInitialMockTweets().FirstOrDefault(x => x.TweetId == id);

            _tweetRepositoryMock.Setup(x => x.GetTweetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(initialTweet);
            _tweetRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(true));

            // Act
            var result = _tweetService.DeleteTweetByIdAsync(id);

            // Assert
            Assert.IsTrue(result.Result);
            _tweetRepositoryMock.Verify(mock => mock.DeleteTweet(initialTweet), Times.Once);
        }

        private void SetIdentifierToUser(string id, string role)
        {
            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, role)
            }));

            var httpContext = new DefaultHttpContext
            {
                User = fakeUser
            };

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
        }

        private void SetNullIdentifierToUser()
        {
            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.Role, RolesConstants.Customer)
            }));

            var httpContext = new DefaultHttpContext
            {
                User = fakeUser
            };

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
        }

        private List<Tweet> GetInitialMockTweets()
        {
            var fakeTweets = new List<Tweet>{
                new Tweet
                {
                    TweetId = "1",
                    Text = "Test tweet 1",
                    Timestamp = DateTime.Now.ToUniversalTime(),
                    Tags = new List<string>
                    {
                        "TestTag1",
                        "TestTag1",
                        "TestTag3"

                    },
                    UserTweets = new List<UserTweets>
                    {
                        new UserTweets
                        {
                            UserId = "1",
                            User = new ApplicationUser
                            {
                                Id = "1",
                                UserName = "TestUser1",
                                Email = "test@test.com"
                            }
                        }
                    }
                },
                new Tweet
                {
                    TweetId = "2",
                    Text = "Test tweet 2",
                    Timestamp = DateTime.Parse("2023-12-11"),
                    Tags = new List<string>
                    {
                        "TestTag2",
                        "TestTag3",
                        "TestTag4"

                    },
                    UserTweets = new List<UserTweets>
                    {
                        new UserTweets
                        {
                            UserId = "2",
                            User = new ApplicationUser
                            {
                                Id = "2",
                                UserName = "TestUser2",
                                Email = "test@test2.com"
                            }
                        }
                    }
                }

            };

            return fakeTweets;
        }
    }
}
