using ExperimentalApp.Core.Constants;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ExperimentalApp.Controllers
{
    ///<summary>
    /// Represents the controller for managing user accounts.
    /// </summary>
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign-in manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerViewModel">The register view model.</param>
        /// <returns>The result of the registration.</returns>
        [HttpPost]
        [Route("account/register")]
        [Authorize(Roles = $"{RolesConstants.Admin},{RolesConstants.Customer}")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                };           

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Ok();
                }

                AddErrors(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Represents the login action.
        /// </summary>
        /// <param name="loginViewModel">Represents login view model</param>
        /// <returns>Returns the user who is logged in</returns>
        [HttpPost]
        [Route("account/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {              
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, ErrorConstants.InvalidLoginAttempt);

                    return BadRequest();
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok();
                }

                ModelState.AddModelError(string.Empty, ErrorConstants.InvalidLoginAttempt);

                return BadRequest();
            }

            return BadRequest();
        }

        /// <summary>
        /// Represents the logout action.
        /// </summary>
        /// <returns>Status code</returns>
        [HttpPost]
        [Route("account/logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        /// <summary>
        /// Represents the action for deactivating an account.
        /// </summary>
        /// <param name="userId">User ID that will be deactivated</param>
        /// <returns>Returns a status code depending on the result of the performed operation</returns>
        [HttpPost]
        [Route("account/deactivate-user")]
        [Authorize(Roles = $"{RolesConstants.Admin},{RolesConstants.Customer}")]
        public async Task<IActionResult> DeactivateAccount(string userId)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(loggedInUserId == userId && User.IsInRole(RolesConstants.Admin))
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound();
            }

            if(await _userManager.IsInRoleAsync(user, RolesConstants.Admin))
            {
                return Forbid();
            }

            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                return Ok();
            }

            ModelState.AddModelError(string.Empty, ErrorConstants.DeactivateAccountError);

            return BadRequest();
        }

        /// <summary>
        /// Represents the action for deleting a user`s roles.
        /// </summary>
        /// <param name="deleteRolesViewModel">View model where specified user id and roles to delete</param>
        /// <returns>Status code depending on the result of the operation</returns>
        [HttpPost]
        [Route("account/delete-user-roles")]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> DeleteUserRoles([FromBody] DeleteUserRolesViewModel deleteRolesViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(deleteRolesViewModel.UserId);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, ErrorConstants.ErrorWhileDeletingUserRoles);

                    return BadRequest();
                }

                if (await _userManager.IsInRoleAsync(user, RolesConstants.Admin))
                {
                    return Forbid();
                }

                var rolesToDelete = _roleManager.Roles.Where(r => deleteRolesViewModel.RolesIds!.Contains(r.Id)).Select(r => r.Name).ToList();
                var result = await _userManager.RemoveFromRolesAsync(user, rolesToDelete);

                if (result.Succeeded)
                {
                    return Ok();
                }

                AddErrors(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Represents the action for getting users.
        /// </summary>
        /// <param name="field">Field by which you need to find a user</param>
        /// <param name="limit">Limit on number of users</param>
        /// <returns>The list of users.</returns>
        [HttpGet]
        [Route("account/get-users")]
        [Authorize(Roles = RolesConstants.Admin)]
        public IActionResult GetUsers(string? field = null, int limit = 0)
        {
            var users = _userManager.Users;

            if (!string.IsNullOrEmpty(field))
            {
                users = users
                    .Where(u => u.UserName.Contains(field) ||
                        u.Email.Contains(field) ||
                        u.FirstName.Contains(field) ||
                        u.LastName.Contains(field));
            }

            if (limit > 0)
            {
                users = users.Take(limit);
            }              

            return Ok(users.ToList());
        }

        /// <summary>
        /// Handle errors from user registration.
        /// </summary>
        /// <param name="result">Result of the request from the Action</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { 
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
