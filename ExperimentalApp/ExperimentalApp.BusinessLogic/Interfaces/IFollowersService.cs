using ExperimentalApp.Core.Models;

namespace ExperimentalApp.BusinessLogic.Interfaces
{
    /// <summary>
    /// Represents a service for manipulation with followers.
    /// </summary>
    public interface IFollowersService
    {     
        /// <summary>
        /// Adds a follower to the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="followerId">The ID of the follower.</param>
        /// <returns>The task result contains a boolean value indicating whether the follower was successfully added.</returns>
        public Task<bool> AddFollowerAsync(string userId, string followerId);

        /// <summary>
        /// Deletes a follower by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="followerId">The ID of the follower.</param>
        /// <returns>The task result is a boolean indicating whether the follower was successfully deleted.</returns>
        public Task<bool> DeleteFollowerByIdAsync(string userId, string followerId);

        /// <summary>
        /// Represents an method for getting all followers by user id.
        /// </summary>
        /// <param name="userId">User id for search followers</param>
        /// <returns>User`s followers</returns>
        public Task<IEnumerable<UserFollowers>> GetFollowersByIdAsync(string userId);
    }
}
