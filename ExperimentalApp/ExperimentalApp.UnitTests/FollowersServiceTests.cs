using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.BusinessLogic.Services;
using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.DataAccessLayer.Interfaces;
using ExperimentalApp.UnitTests.Fakes;
using Moq;

namespace ExperimentalApp.UnitTests
{
    [TestFixture]
    public class FollowersServiceTests
    {
        private Mock<IFollowersRepository> _followersRepositoryMock;
        private Mock<FakeUserManager> _userManagerMock;
        private IFollowersService _followersService;

        [SetUp]
        public void Setup()
        {
            _followersRepositoryMock = new Mock<IFollowersRepository>();
            _userManagerMock = new Mock<FakeUserManager>();
            _followersService = new FollowersService(_followersRepositoryMock.Object, _userManagerMock.Object);
        }

        [Test]
        public async Task AddFollowerAsync_WhenUserNotFound_AddFollowersNotCallAndReturnsFalse()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<ApplicationUser>(null));

            // Act
            var result = await _followersService.AddFollowerAsync("1", "2");

            // Assert
            Assert.IsFalse(result);
            _followersRepositoryMock.Verify(x => x.AddFollower(It.IsAny<UserFollowers>()), Times.Never);
        }

        [Test]
        public async Task AddFollowerAsync_WhenFollowerNotFound_AddFollowersNotCallAndReturnsFalse()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new ApplicationUser()));
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<ApplicationUser>(null));

            // Act
            var result = await _followersService.AddFollowerAsync("1", "2");

            // Assert
            Assert.IsFalse(result);
            _followersRepositoryMock.Verify(x => x.AddFollower(It.IsAny<UserFollowers>()), Times.Never);
        }

        [Test]
        public async Task AddFollowerAsync_WhenRelationExist_AddFollowersNotCallAndReturnsFalse()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new ApplicationUser()));
            _followersRepositoryMock.Setup(x => x.DoesUserHaveFollowerAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            // Act
            var result = await _followersService.AddFollowerAsync("1", "2");

            // Assert
            Assert.IsFalse(result);
            _followersRepositoryMock.Verify(x => x.AddFollower(It.IsAny<UserFollowers>()), Times.Never);
        }

        [Test]
        public async Task AddFollowerAsync_WhenAllIsNotNullAndRelationNotExist_AddFollowersCallAndReturnsTrue()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new ApplicationUser()));
            _followersRepositoryMock.Setup(x => x.DoesUserHaveFollowerAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(false));
            _followersRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(true));

            // Act
            var result = await _followersService.AddFollowerAsync("1", "2");

            // Assert
            Assert.IsTrue(result);
            _followersRepositoryMock.Verify(x => x.AddFollower(It.IsAny<UserFollowers>()), Times.Once);
        }

        [Test]
        public async Task DeleteFollowerByIdAsync_WhenDeleteSuccessed_ReturnsTrue()
        {
            // Arrange
            _followersRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(true));

            // Act
            var result = await _followersService.DeleteFollowerByIdAsync("1", "2");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteFollowerByIdAsync_WhenDeleteFailed_ReturnsFalse()
        {
            // Arrange
            _followersRepositoryMock.Setup(x => x.Complete()).Returns(Task.FromResult(false));

            // Act
            var result = await _followersService.DeleteFollowerByIdAsync("1", "2");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetFollowersByIdAsync_WhenCalled_ReturnsFollowers()
        {
            // Arrange
            _followersRepositoryMock.Setup(x => x.GetFollowersByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<IEnumerable<UserFollowers>>(new List<UserFollowers>()));

            // Act
            var result = await _followersService.GetFollowersByIdAsync("1");

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
