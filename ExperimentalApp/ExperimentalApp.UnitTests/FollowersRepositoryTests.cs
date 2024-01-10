using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.DataAccessLayer.DataDbContext;
using ExperimentalApp.DataAccessLayer.Interfaces;
using ExperimentalApp.DataAccessLayer.Repositories;
using ExperimentalApp.UnitTests.Fakes;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace ExperimentalApp.UnitTests
{
    public class FollowersRepositoryTests
    {
        private Mock<DvdRentalContext> _dbContextMock;
        private IFollowersRepository _followerRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<DvdRentalContext>();
            _dbContextMock.Setup(x => x.UserFollowers).ReturnsDbSet(GetFakeUserFollowers());
            _followerRepository = new FollowersRepository(_dbContextMock.Object);
        }

        [Test]
        public void AddFollower_WhenCalled_AddFollower()
        {
            // Arrange
            var follower = new UserFollowers
            {
                FollowingId = "1",
                FollowerId = "3"
            };

            // Act
            _followerRepository.AddFollower(follower);

            // Assert
            _dbContextMock.Verify(x => x.UserFollowers.Add(follower), Times.Once);
        }

        [Test]
        public async Task DoesUserHaveFollowerAsync_WhenUserHasFollower_ReturnsTrue()
        {
            // Arrange
            var userId = "1";
            var followerId = "2";

            // Act
            var result = await _followerRepository.DoesUserHaveFollowerAsync(userId, followerId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesUserHaveFollowerAsync_WhenUserHasNotFollower_ReturnsFalse()
        {
            // Arrange
            var userId = "1";
            var followerId = "3";

            // Act
            var result = await _followerRepository.DoesUserHaveFollowerAsync(userId, followerId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteFollowerByIdAsync_WhenFollowerNotFound_DeleteFollowerNotCall()
        {
            // Arrange
            var userId = "1";
            var followerId = "3";
            _dbContextMock.Setup(x => x.UserFollowers).ReturnsDbSet(GetFakeUserFollowers().BuildMock());

            // Act
            await _followerRepository.DeleteFollowerByIdAsync(userId, followerId);

            // Assert
            _dbContextMock.Verify(x => x.UserFollowers.Remove(It.IsAny<UserFollowers>()), Times.Never);
        }

        [Test]
        public async Task DeleteFollowerByIdAsync_WhenFollowingNotFound_DeleteFollowerNotCall()
        {
            // Arrange
            var userId = "3";
            var followerId = "1";
            _dbContextMock.Setup(x => x.UserFollowers).ReturnsDbSet(GetFakeUserFollowers().BuildMock());

            // Act
            await _followerRepository.DeleteFollowerByIdAsync(userId, followerId);

            // Assert
            _dbContextMock.Verify(x => x.UserFollowers.Remove(It.IsAny<UserFollowers>()), Times.Never);
        }

        [Test]
        public async Task DeleteFollowerByIdAsync_WhenFollowerFound_DeleteFollowerCall()
        {
            // Arrange
            var userId = "1";
            var followerId = "2";
            _dbContextMock.Setup(x => x.UserFollowers).ReturnsDbSet(GetFakeUserFollowers().BuildMock());

            // Act
            await _followerRepository.DeleteFollowerByIdAsync(userId, followerId);

            // Assert
            _dbContextMock.Verify(x => x.UserFollowers.Remove(It.IsAny<UserFollowers>()), Times.Once);
        }

        [Test]
        public async Task GetFollowersByIdAsync_WhenCalled_ReturnsFollowers()
        {
            // Arrange
            var userId = "1";
            var fakeFollowers = GetFakeUserFollowers().BuildMock();
            _dbContextMock.Setup(x => x.UserFollowers).ReturnsDbSet(fakeFollowers);

            // Act
            var result = await _followerRepository.GetFollowersByIdAsync(userId);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        private IEnumerable<UserFollowers> GetFakeUserFollowers()
        {
            return new List<UserFollowers>
            {
                new UserFollowers
                {
                    FollowingId = "1",
                    FollowerId = "2"
                },
                new UserFollowers
                {
                    FollowingId = "2",
                    FollowerId = "1"
                }
            };
        }
    }
}
