using ExperimentalApp.Controllers;
using ExperimentalApp.Core.Constants;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.Core.ViewModels;
using ExperimentalApp.UnitTests.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using Tests.DataLayers.Fakes;
using MockQueryable.Moq;

namespace ExperimentalApp.UnitTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<FakeSignInManager> _mockSignInManager;
        private Mock<FakeUserManager> _mockUserManager;
        private Mock<FakeRoleManager> _mockRoleManager;
        private AccountController _controller;

        [SetUp]
        public void Setup()
        {
            _mockSignInManager = new Mock<FakeSignInManager>();
            _mockUserManager = new Mock<FakeUserManager>();          
            _mockRoleManager = new Mock<FakeRoleManager>();

            _controller = new AccountController(_mockSignInManager.Object, _mockUserManager.Object, _mockRoleManager.Object);
        }

        [Test]
        public void Register_WhenCorrectValues_ReturnsOkResult()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel()
            {
                Email = "test@gmail.com",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Password = "Test!123",
                ConfirmPassword = "Test!123",
                UserName = "TestUserName"
            };

            SetUserWithRole(RolesConstants.Admin);

            _mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            SimulateValidation(registerViewModel);
            var result = _controller.Register(registerViewModel);

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void Register_WhenModelStateIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel()
            {
                Email = "test",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Password = "Test!123",
                ConfirmPassword = "Test!",
                UserName = "TestUserName"
            };

            SetUserWithRole(RolesConstants.Admin);

            // Act
            SimulateValidation(registerViewModel);
            var result = _controller.Register(registerViewModel);

            // Assert   
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void Register_WhenUserCreationFails_ReturnsBadRequestResult()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel()
            {
                Email = "test@test.com",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Password = "Test!123",
                ConfirmPassword = "Test!123",
                UserName = "TestUserName"
            };

            SetUserWithRole(RolesConstants.Admin);

            _mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" })); 

            // Act  
            SimulateValidation(registerViewModel);
            var result = _controller.Register(registerViewModel);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.IsTrue(_controller.ModelState.ErrorCount > 0);
        }

        [Test]
        public void Login_WhenCorrectValues_ReturnsOkResult()
        {
            // Arrange
            var loginViewModel = new LoginViewModel()
            {
                Email = "user@example.com",
                Password = "Test!123",
                RememberMe = true
            };

            _mockUserManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            _mockSignInManager
                .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            SimulateValidation(loginViewModel);
            var result = _controller.Login(loginViewModel);

            //Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void Login_WhenModelStateIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var loginViewModel = new LoginViewModel()
            {
                Email = "user",
                Password = "Test!123",
                RememberMe = true
            };

            // Act
            SimulateValidation(loginViewModel);
            var result = _controller.Login(loginViewModel);

            //Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void Login_WhenUserNotFound_ReturnsBadRequestResult()
        {
            // Arrange
            var loginViewModel = new LoginViewModel()
            {
                Email = "user@toto.com",
                Password = "Test!123",
                RememberMe = true
            };

            _mockUserManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            SimulateValidation(loginViewModel);
            var result = _controller.Login(loginViewModel);

            //Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.IsTrue(_controller.ModelState.ErrorCount > 0);
        }

        [Test]
        public void Login_WhenPasswordSignInFails_ReturnsBadRequestResult()
        {
            // Arrange
            var loginViewModel = new LoginViewModel()
            {
                Email = "user@example.com",
                Password = "Test!123",
                RememberMe = true
            };

            _mockUserManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());   

            _mockSignInManager
                .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);  

            //Act
            SimulateValidation(loginViewModel);
            var result = _controller.Login(loginViewModel);

            //Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.IsTrue(_controller.ModelState.ErrorCount > 0);
        }

        [Test]
        public void Logout_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockSignInManager
                .Setup(x => x.SignOutAsync())
                .Returns(Task.FromResult(0));

            // Act
            var result = _controller.LogOut();

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void Register_WhenUserIsNotAdmin_ReturnsOkResultResult()
        {
            //Arange
            var registerViewModel = new RegisterViewModel()
            {
                Email = "test@test.com",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Password = "Test!123",
                ConfirmPassword = "Test!123",
                UserName = "TestUserName"
            };

            SetUserWithRole(RolesConstants.Customer);

            _mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            SimulateValidation(registerViewModel);
            var result = _controller.Register(registerViewModel);

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void DeactivateAccount_WhenDeactivateCustomer_ReturnsOkResultResult()
        {
            //Arange
            var userId = "1";
            SetIdentifierToUser("2", RolesConstants.Customer);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser { });

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            //Act
            var result = _controller.DeactivateAccount(userId);

            //Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void DeactivateAccount_WhenDeactivateAdmin_ReturnsBadRequestResult()
        {
            //Arange
            var userId = "1";
            SetIdentifierToUser("2", RolesConstants.Customer);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser { });

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            //Act
            var result = _controller.DeactivateAccount(userId);

            //Assert
            Assert.IsInstanceOf<ForbidResult>(result.Result);
            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Test]
        public void DeactivateAccount_WhenUserNotFound_ReturnsNotFoundResult()
        {
            //Arange
            var userId = "1";
            SetIdentifierToUser("2", RolesConstants.Customer);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            //Act
            var result = _controller.DeactivateAccount(userId);

            //Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Test]
        public void DeleteUserRoles_WhenUserIsNotAdmin_ReturnsOkResultResult()
        {
            //Arange
            var deleteUserRolesViewModel = new DeleteUserRolesViewModel()
            {
                UserId = "1",
                RolesIds = new List<string>() { "2" }
            };

            SetMockRoles();

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), RolesConstants.Admin ))
                .ReturnsAsync(false);

            _mockUserManager.Setup(x => x.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), new List<string> { RolesConstants.Customer }))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            SimulateValidation(deleteUserRolesViewModel);
            var result = _controller.DeleteUserRoles(deleteUserRolesViewModel);

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);            
        }

        [Test]
        public void DeleteUserRoles_WhenUserIsAdmin_ReturnsForbidResult()
        {
            //Arange
            var deleteUserRolesViewModel = new DeleteUserRolesViewModel()
            {
                UserId = "1",
                RolesIds = new List<string>() { "1", "2" }
            };

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), RolesConstants.Admin))
                .ReturnsAsync(true);

            // Act
            SimulateValidation(deleteUserRolesViewModel);
            var result = _controller.DeleteUserRoles(deleteUserRolesViewModel);

            // Assert
            Assert.IsInstanceOf<ForbidResult>(result.Result);
            _mockUserManager.Verify(x => x.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void DeleteUserRoles_WhenModelStateIsInvalid_ReturnsBadRequestResult()
        {
            //Arange
            var deleteUserRolesViewModel = new DeleteUserRolesViewModel()
            {
                UserId = "1",
                RolesIds = null
            };

            // Act
            SimulateValidation(deleteUserRolesViewModel);
            var result = _controller.DeleteUserRoles(deleteUserRolesViewModel);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            _mockUserManager.Verify(x => x.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]  
        public void DeactivateAccount_WhenDeactivateCustomerFails_ReturnsBadRequestResult()
        {
            //Arange
            var userId = "1";
            SetIdentifierToUser("2", RolesConstants.Customer);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser { });

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            //Act
            var result = _controller.DeactivateAccount(userId);

            //Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.IsTrue(_controller.ModelState.ErrorCount > 0);
        }

        [Test]
        public void DeactivateAccount_WhenUserToDeactivateIsAdminAndIsSameWithLoggedInUser_ReturnsForbidResult()
        {
            //Arange
            var userId = "1";
            SetIdentifierToUser(userId, RolesConstants.Admin);

            //Act
            var result = _controller.DeactivateAccount(userId);

            //Assert
            Assert.IsInstanceOf<ForbidResult>(result.Result);
            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Test]
        public void DeleteUserRoles_WhenUserNotFound_ReturnsBadRequestResult()
        {
            //Arange
            var deleteUserRolesViewModel = new DeleteUserRolesViewModel()
            {
                UserId = "1",
                RolesIds = new List<string>() { "1", "2" }
            };

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            SimulateValidation(deleteUserRolesViewModel);
            var result = _controller.DeleteUserRoles(deleteUserRolesViewModel);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.IsTrue(_controller.ModelState.ErrorCount > 0);
            _mockUserManager.Verify(x => x.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void DeleteUserRoles_WhenRemovingRolesFails_ReturnsBadRequestResult()
        {
            //Arange
            var deleteUserRolesViewModel = new DeleteUserRolesViewModel()
            {
                UserId = "1",
                RolesIds = new List<string>() { "1", "2" }
            };

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), RolesConstants.Admin))
                .ReturnsAsync(false);

            _mockUserManager.Setup(x => x.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act
            SimulateValidation(deleteUserRolesViewModel);
            var result = _controller.DeleteUserRoles(deleteUserRolesViewModel);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            Assert.IsTrue(_controller.ModelState.ErrorCount > 0);
        }

        [Test]
        public async Task GetUsers_WhenCalled_ReturnsListOfUsers()
        {
            // Arrange
            SetMockUsers();

            // Act
            var result = _controller.GetUsers() as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<List<ApplicationUser>>(result.Value);
        }

        [Test]
        public async Task GetUsers_WhenCalledWithParams_ReturnsFilteredListOfUsers()
        {
            // Arrange
            SetMockUsers();

            var field = "user1";
            var limit = 2;

            // Act
            var result = _controller.GetUsers(field, limit) as OkObjectResult;
            var users = (List<ApplicationUser>)result.Value;

            // Assert
            Assert.That(((List<ApplicationUser>)result.Value).Count, Is.LessThanOrEqualTo(limit));
            users.ForEach(x => Assert.That(x.UserName, Is.EqualTo(field)));
        }

        [Test]
        public async Task GetUsers_WhenNoUsers_ReturnsNotFoundResult()
        {
            // Arrange
            var mockUsers = new List<ApplicationUser>().BuildMock();

            _mockUserManager
                .Setup(x => x.Users)
                .Returns(mockUsers);

            // Act
            var result = _controller.GetUsers() as OkObjectResult;

            // Assert
            Assert.That((List<ApplicationUser>)result.Value, Is.Empty);
        }

        private void SimulateValidation(object model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }

        private void SetUserWithRole(string role)
        {
            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
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

        private void SetMockRoles()
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "1",
                    Name = RolesConstants.Admin
                },
                new IdentityRole()
                {
                    Id = "2",
                    Name = RolesConstants.Customer
                }
            };

            var mockRoles = roles.BuildMock();

            _mockRoleManager.Setup(x => x.Roles).Returns(mockRoles);
        }

        private void SetMockUsers()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "user4", Email = "user4@example.com", FirstName = "John", LastName = "Doe" },
                new ApplicationUser { UserName = "user3", Email = "user3@example.com", FirstName = "John", LastName = "Doe" },
                new ApplicationUser { UserName = "user1", Email = "user1@example.com", FirstName = "John", LastName = "Doe" },
                new ApplicationUser { UserName = "user1", Email = "user1@example.com", FirstName = "John", LastName = "Doe" },
                new ApplicationUser { UserName = "user2", Email = "user2@example.com", FirstName = "Jane", LastName = "Doe" },
                new ApplicationUser { UserName = "user1", Email = "user1@example.com", FirstName = "John", LastName = "Doe" },
                new ApplicationUser { UserName = "user5", Email = "user5@example.com", FirstName = "John", LastName = "Doe" }
            };

            var mockUsers = users.BuildMock();

            _mockUserManager
                .Setup(x => x.Users)
                .Returns(mockUsers);
        }
    }
}
