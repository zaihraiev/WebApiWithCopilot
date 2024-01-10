using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.Controllers;
using ExperimentalApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace ExperimentalApp.UnitTests
{
    [TestFixture]
    public class FollowersControllerTests
    {
        private FollowersController _controller;
        private Mock<IFollowersService> _followersServiceMock;

        [SetUp]
        public void Setup()
        {
            _followersServiceMock = new Mock<IFollowersService>();
            _controller = new FollowersController(_followersServiceMock.Object);
        }

        [Test]
        public void CreateFollower_WhenUpdateFailed_ReturnsBadRequest()
        {
            // Arrange
            SetIdentifierToUser("1", "Customer");
            _followersServiceMock.Setup(x => x.AddFollowerAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(false));

            // Act
            var result = _controller.Follow("2");

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void CreateFollower_WhenUpdateSucceeded_ReturnsOk()
        {
            // Arrange
            SetIdentifierToUser("1", "Customer");
            _followersServiceMock.Setup(x => x.AddFollowerAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            // Act
            var result = _controller.Follow("2");

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void DeleteFollower_WhenDeleteFailed_ReturnsBadRequest()
        {
            // Arrange
            SetIdentifierToUser("1", "Customer");
            _followersServiceMock.Setup(x => x.DeleteFollowerByIdAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(false));

            // Act
            var result = _controller.DeleteFollower("2");

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void DeleteFollower_WhenDeleteSucceeded_ReturnsOk()
        {
            // Arrange
            SetIdentifierToUser("1", "Customer");
            _followersServiceMock.Setup(x => x.DeleteFollowerByIdAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            // Act
            var result = _controller.DeleteFollower("2");

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void GetFollowersById_WhenCalled_ReturnsOk()
        {
            // Arrange
            _followersServiceMock.Setup(x => x.GetFollowersByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<IEnumerable<UserFollowers>>(new List<UserFollowers>()));  

            // Act
            var result = _controller.GetFollowersById("1");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        private void SetIdentifierToUser(string id, string role)
        {
            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, role)
            }));

            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = fakeUser
                }
            };

            _controller.ControllerContext = context;
        }
    }
}
