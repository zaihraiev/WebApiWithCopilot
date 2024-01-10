using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.DataAccessLayer.DataDbContext;
using ExperimentalApp.DataAccessLayer.Interfaces;
using ExperimentalApp.DataAccessLayer.Repositories;
using Moq.EntityFrameworkCore;
using Moq;
using ExperimentalApp.Core.ViewModels;

namespace ExperimentalApp.UnitTests
{
    [TestFixture]
    public class TweetsRepositoryTests
    {
        private Mock<DvdRentalContext> _dbContextMock;
        private ITweetRepository _tweetRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<DvdRentalContext>();
            _dbContextMock.Setup(x => x.Tweets).ReturnsDbSet(GetFakeTweets());
            _tweetRepository = new TweetRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetTweetByIdAsync_WhenTweetExists_ReturnsTweet()
        {
            // Arrange
            var id = "2";

            //Act
            var result = await _tweetRepository.GetTweetByIdAsync(id);

            //Assert
            Assert.That(result.TweetId, Is.EqualTo(id));
        }

        [Test]
        public async Task GetTweetsAsync()
        {
            //Arrange
            var initialTweets = GetFakeTweets();           

            //Act
            var result = await _tweetRepository.GetTweetsAsync();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(initialTweets.Count()));
            Assert.IsTrue(initialTweets.Zip(result, (i, r) => i.TweetId == r.TweetId).All(x => x));
        }

        [Test]
        public void GetFilteredTweetsAsync_WhenFilterByText_ReturnsNotEmptyList()
        {
            // Arrange
            _dbContextMock.Setup(x => x.Tweets).ReturnsDbSet(GetFakeTweets());
            var textFilter = "Test text 2";

            // Act
            var result = _tweetRepository.GetFilteredTweetsAsync(new TweetFilterViewModel { Text = textFilter });

            // Assert
            Assert.IsTrue(result.Result.Any());
            Assert.That(result.Result.All(x => x.Text.ToLower().Contains(textFilter.ToLower())));
        }

        [Test]
        public void GetFilteredTweetsAsync_WhenFilterByTag_ReturnsNotEmptyList()
        {
            // Arrange
            _dbContextMock.Setup(x => x.Tweets).ReturnsDbSet(GetFakeTweets());
            var tagFilter = "TestTag1";

            // Act
            var result = _tweetRepository.GetFilteredTweetsAsync(new TweetFilterViewModel { Tags = new List<string> { tagFilter } });
            Assert.IsTrue(result.Result.Any());
            Assert.That(result.Result.All(x => x.Tags.Any(t => t == tagFilter)));
        }

        [Test]
        public void GetFilteredTweetsAsync_WhenFilterByUserName_ReturnsNotEmptyList()
        {
            // Arrange
            _dbContextMock.Setup(x => x.Tweets).ReturnsDbSet(GetFakeTweets());
            var userNameFilter = "TestUser1";

            // Act
            var result = _tweetRepository.GetFilteredTweetsAsync(new TweetFilterViewModel { UserName = userNameFilter });

            // Assert
            Assert.IsTrue(result.Result.Any());
            Assert.That(result.Result.All(x => x.UserTweets.Any(ut => ut.User.UserName == userNameFilter)));
        }

        [Test]
        public void GetFilteredTweetsAsync_WhenFilterByTimestamp_ReturnsNotEmptyList()
        {
            // Arrange
            _dbContextMock.Setup(x => x.Tweets).ReturnsDbSet(GetFakeTweets());
            var timestampFilter = new DateTime(2020, 1, 1);

            // Act
            var result = _tweetRepository.GetFilteredTweetsAsync(new TweetFilterViewModel { Timestamp = timestampFilter });

            // Assert
            Assert.IsTrue(result.Result.Any());
            Assert.That(result.Result.All(x => x.Timestamp == timestampFilter));
        }

        private IEnumerable<Tweet> GetFakeTweets()
        {
            return new List<Tweet>
            {
                new Tweet
                {
                    TweetId = "1",
                    Text = "Test text",
                    Tags = new List<string>
                    {
                        "TestTag1",
                        "TestTag2",
                        "TestTag3"
                    },
                    Timestamp = new DateTime(2020, 1, 1),
                    UserTweets = new List<UserTweets>
                    {
                        new UserTweets
                        {
                            UserId = "1",
                            User = new ApplicationUser
                            {
                                Id = "1",
                                UserName = "TestUser1" 
                            }
                        }
                    }
                },
                new Tweet
                {
                    TweetId = "2",
                    Text = "Test text 2",
                    Tags = new List<string>
                    {
                        "TestTag4",
                        "TestTag5",
                        "TestTag6"
                    },
                    Timestamp = new DateTime(2021, 1, 1),
                    UserTweets = new List<UserTweets>
                    {
                        new UserTweets
                        {
                            UserId = "2",
                            User = new ApplicationUser
                            {
                                Id = "2",
                                UserName = "TestUser2"
                            }
                        }
                    }
                }
            };
        }
    }
}
