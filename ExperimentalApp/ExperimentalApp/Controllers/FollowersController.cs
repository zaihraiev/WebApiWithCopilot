using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExperimentalApp.Controllers
{
    /// <summary>
    /// Represents a controller for followers.
    /// </summary>
    [Authorize]
    public class FollowersController : ControllerBase
    {
        private readonly IFollowersService _followersService;

        /// <summary>
        /// Constructs a new instance of the <see cref="FollowersController"/> class with the specified followers service.
        /// </summary>
        /// <param name="followersService">Service for working with followers</param>
        public FollowersController(IFollowersService followersService)
        {
            _followersService = followersService;
        }

        /// <summary>
        /// Represents an endoint for adding a follower and following a user.
        /// </summary>
        /// <param name="userId">The user who performs the action</param>
        /// <param name="followerId">The user to perform the action on</param>
        /// <returns>Result depending on execution result state</returns>
        [HttpPost]
        [Route("followers/{followerId}")]
        [Authorize(Roles = RolesConstants.Customer)]
        public async Task<IActionResult> Follow(string followerId)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _followersService.AddFollowerAsync(loggedInUserId, followerId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a follower by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="followerId">The ID of the follower.</param>
        /// <returns>The task result is a boolean indicating whether the follower was successfully deleted.</returns>
        [HttpDelete]
        [Route("followers/{followerId}")]
        [Authorize(Roles = RolesConstants.Customer)]
        public async Task<IActionResult> DeleteFollower(string followerId)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _followersService.DeleteFollowerByIdAsync(loggedInUserId, followerId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Represents an endpoint for getting all followers by user id.
        /// </summary>
        /// <param name="userId">User id for search followers</param>
        /// <returns>User`s followers</returns>
        [HttpGet]
        [Route("followers/{userId}")]
        public async Task<IActionResult> GetFollowersById(string userId)
        {
            var followers = await _followersService.GetFollowersByIdAsync(userId);

            return Ok(followers);
        }
    }
}
