using ExperimentalApp.Core.Models;

namespace ExperimentalApp.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Represents a repository for managing user data.
    /// </summary>
    public interface IFollowersRepository
    {
        /// <summary>
        /// Adds a follower to the user's followers list.
        /// </summary>
        /// <param name="userFollowers">The user followers object.</param>
        public void AddFollower(UserFollowers userFollowers);

        /// <summary>
        /// Completes the asynchronous task.
        /// </summary>
        /// <returns>A task representing the completion status.</returns>
        public Task<bool> Complete();

        /// <summary>
        /// Checks if a relation exists between the specified user and follower.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="followerId">The ID of the follower.</param>
        /// <returns>A task representing the existence of the relation.</returns>
        public Task<bool> DoesUserHaveFollowerAsync(string userId, string followerId);

        /// <summary>
        /// Deletes a follower by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="followerId">The ID of the follower</param>
        public Task DeleteFollowerByIdAsync(string userId, string followerId);

        /// <summary>
        /// Represents an method for getting all followers by user id.
        /// </summary>
        /// <param name="userId">User id for search followers</param>
        /// <returns>User`s followers</returns>
        public Task<IEnumerable<UserFollowers>> GetFollowersByIdAsync(string userId);
    }
}
